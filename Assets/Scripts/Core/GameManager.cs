using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private EnemyAIManager aIManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField][Range(1, 5)] private int wallsPerTurn;
    [SerializeField][Range(1, 10)] private int enemyMovesPerTurn;
    [SerializeField][Range(1, 3)] private int secondsBetweenEnemyMoves;
    [SerializeField][Range(1, 15)] private int goalTurnCount;
    [SerializeField] private List<Hint> hints;

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
        gameState.gameOver = true;
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
        }
    }

    public void GetHint()
    {
        hintIndex++;
        for (int i = 0; i < hintIndex; i++)
        {
            Hint hint = hints[i];
            uiManager.ShowHintWall(hint);
        }
        uiManager.UpdateUI();
    }

    public void UndoWallBuild()
    {
        if (gameState.wallsBuildCurrentTurn > 0)
        {
            gameState.wallsBuildCurrentTurn--;
            Wall wallToUndo = board.UndoWallBuild();
            uiManager.UpdateUI();
            uiManager.RemoveWall(wallToUndo);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Debug.Log("next level");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
