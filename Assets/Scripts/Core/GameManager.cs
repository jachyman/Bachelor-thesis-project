using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private EnemyAIManager aIManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int wallsPerTurn;
    [SerializeField] private int enemyMovesPerTurn;
    [SerializeField] private int secondsBetweenEnemyMoves;
    [SerializeField] private int goalTurnCount;

    private GameState gameState;

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

    public void StartGame()
    {
        gameState = new GameState(board);
        StartPlayerTurn();
    }
    public void CheckEndConditions()
    {
        foreach (Enemy enemy in board.enemies)
        {
            ITile tile = board.GetTileAt(enemy.Position);
            if (tile is BaseTile baseTile)
            {
                if (baseTile.IsGoal)
                {
                    EndGame(false);
                    return;
                }
            }
        }
        if (gameState.currentTurn > goalTurnCount)
        {
            EndGame(true);
        }
    }
    public void EndGame(bool playerWin)
    {
        gameState.gameOver = true;
        uiManager.GameOver(playerWin);
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
        if (gameState.gameOver)
        {
            return;
        }

        gameState.isPlayerTurn = false;
        uiManager.UpdateUI();
        StartCoroutine(ExecuteEnemyMoves());
    }
    private IEnumerator ExecuteEnemyMoves()
    {
        aIManager.PlanEnemyMovement(uiManager.GetOnGroundTilemap());

        for (int i = 0; i < enemyMovesPerTurn; i++)
        {
            if (i != 0)
            {
                yield return new WaitForSeconds(secondsBetweenEnemyMoves);
            }
            NextEnemyStep();
        }

        StartPlayerTurn();
    }
    public void NextEnemyStep()
    {
        aIManager.ExecuteStepOfPlan();
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
            uiManager.UpdateUI();
            uiManager.AddWall(wall);
            board.AddWall(wall);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Debug.Log("next level");
    }
}
