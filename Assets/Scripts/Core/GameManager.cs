using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private EnemyAIManager aIManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField][Range(0, 5)] private int wallsPerTurn;
    [SerializeField][Range(1, 10)] private int enemyMovesPerTurn;
    [SerializeField][Range(0, 3)] private int secondsBetweenEnemyMoves;
    [SerializeField][Range(1, 15)] private int goalTurnCount;
    [SerializeField] private List<Hint> hints;

    [Header("--------For tutorial levels-------")]
    [Header("")]
    [SerializeField] private UnityEvent gameLostFunction;
    [SerializeField] private UnityEvent wallBuiltFunction;
    [SerializeField] private UnityEvent blockedGoalFunction;
    [SerializeField] private UnityEvent undoWallFunction;
    [SerializeField] private UnityEvent getHintFunction;

    private int hintIndex = 0;
    private GameState gameState;

    [Serializable]
    public class Hint
    {
        public WallType wallType;
        public int x;
        public int y;
    }

    public enum EnemyMovement
    {
        Simultanious,
        NonSimultanious
    }

    public enum GameOver
    {
        PlayerWon,
        PlayerLostEnemyReachedGoal,
        PlayerLostLastTurn,
        PlayerLostGoalBlocked
    }

    public enum WallType
    {
        Vertical,
        Horizontal
    }

    private void Start()
    {
        if (board == null)
        {
            Debug.LogError("board is null in game manager");
        }
        else
        {
            StartGame();
        }
    }

    public int GetCurrentTurn()
    {
        int a = gameState.currentTurn;
        return a;
    }
    public int GetGoalTurnCount()
    {
        return goalTurnCount;
    }
    public bool IsPlayerTurn()
    {
        return gameState.isPlayerTurn;
    }
    public int GetWallsLeft()
    {
        return wallsPerTurn - gameState.wallsBuildCurrentTurn;
    }
    public int GetEnemyMovement()
    {
        return enemyMovesPerTurn;
    }

    public int GetWallsPerTurn() { return wallsPerTurn; }

    public int GetHintIndex() { return hintIndex; }
    public bool DidPlayerWon()
    {
        if (gameState.gameOver && gameState.isPlayerWinner)
        {
            return true;
        }
        return false;
    }

    public void StartGame()
    {
        gameState = new GameState(board);
        StartPlayerTurn();
    }
    public void CheckEndConditions()
    {
        // is enemy on goal tile
        foreach (Enemy enemy in board.enemies)
        {
            ITile tile = board.GetTileAt(enemy.Position);
            if (tile is BaseTile baseTile)
            {
                if (baseTile.IsGoal)
                {
                    EndGame(GameOver.PlayerLostEnemyReachedGoal);
                    return;
                }
            }
        }

        // are there enemies left
        if (!board.IsAnyEnemyAlive())
        {
            EndGame(GameOver.PlayerWon);
            return;
        }

        // is this last round
        if (gameState.currentTurn > goalTurnCount)
        {
            EndGame(GameOver.PlayerLostLastTurn);
            return;
        }
    }
    public void EndGame(GameOver gameOver)
    {
        gameLostFunction?.Invoke();
        if (gameOver == GameOver.PlayerLostGoalBlocked)
        {
            blockedGoalFunction?.Invoke();
        }

        gameState.gameOver = true;
        if (gameOver == GameOver.PlayerWon)
        {
            gameState.isPlayerWinner = true;
        }
        uiManager.GameOver(gameOver);
    }
    public void StartPlayerTurn()
    {
        if (gameState.gameOver)
        {
            return;
        }

        gameState.currentTurn++;
        CheckEndConditions();
        if (!gameState.gameOver)
        {
            gameState.isPlayerTurn = true;
            gameState.wallsBuildCurrentTurn = 0;

            uiManager.UpdateUI();
        }
    }
    public void StartEnemyTurn()
    {
        if (gameState.isPlayerTurn)
        {
            CheckEndConditions();
            if (gameState.gameOver)
            {
                return;
            }

            board.SwitchSwitchTiles(uiManager);

            gameState.isPlayerTurn = false;
            uiManager.UpdateUI();
            StartCoroutine(ExecuteEnemyMoves());
        }
        else
        {
            Debug.Log("enemy turn hasnt finished");
        }
    }
    private IEnumerator ExecuteEnemyMoves()
    {
        bool isEnemyPlanSuccess = aIManager.PlanEnemyMovement(uiManager, enemyMovement);

        if (isEnemyPlanSuccess)
        {
            int enemyMovesCount = enemyMovement == EnemyMovement.Simultanious ? (enemyMovesPerTurn * board.GetAliveEnemyCount()) : enemyMovesPerTurn;
            for (int i = 0; i < enemyMovesCount; i++)
            {
                if (i != 0)
                {
                    yield return new WaitForSeconds(secondsBetweenEnemyMoves);
                }
                NextEnemyStep();
                if (gameState.gameOver)
                {
                    break;
                }
            }
            StartPlayerTurn();
        }
        else
        {
            EndGame(GameOver.PlayerLostGoalBlocked);
        }
    }
    
    public void NextEnemyStep()
    {
        aIManager.ExecuteStepOfPlan();
        CheckEndConditions();
    }
    public bool CanBuildWallHere(Wall wall)
    {
        if (!board.CanBuildWallHere(wall))
        {
            Debug.Log("cannot build on this board place");
            return false;
        }
        if (gameState.wallsBuildCurrentTurn >= wallsPerTurn)
        {
            Debug.Log("out of walls for this turn");
            return false;
        }
        if (!gameState.isPlayerTurn)
        {
            Debug.Log("isnt player turn");
            return false;
        }
        return true;
    }
    public void BuildWall(Wall wall)
    {
        if (CanBuildWallHere(wall))
        {
            gameState.wallsBuildCurrentTurn++;
            uiManager.AddWall(wall);
            board.AddWall(wall);
            wallBuiltFunction?.Invoke();
        }
    }

    public void GetHint()
    {
        getHintFunction?.Invoke();

        if (hintIndex < hints.Count)
        {
            uiManager.ShowHintWall(hints[hintIndex]);
            hintIndex++;
        }
    }

    public void UndoWallBuild()
    {
        if (gameState.wallsBuildCurrentTurn > 0)
        {
            undoWallFunction?.Invoke();

            gameState.wallsBuildCurrentTurn--;
            Wall wallToUndo = board.UndoWallBuild();
            uiManager.UpdateUI();
            uiManager.RemoveWall(wallToUndo);
        }
    }
}
