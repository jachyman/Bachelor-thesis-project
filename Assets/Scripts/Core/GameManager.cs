using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private EnemyAIManager aIManager;
    [SerializeField] private UIManager uiManager;

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

    public void StartGame()
    {
        gameState = new GameState(board);
        aIManager.PlanEnemyMovement(uiManager.GetOnGroundTilemap());
    }
    public void EndGame()
    {

    }
    public void StartPlayerTurn()
    {

    }
    public void StartEnemyTurn()
    {

    }
    public void NextEnemyStep()
    {
        aIManager.ExecuteStepOfPlan();
    }
}
