using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private Board board;

    private const string PDDLFileName = "new_structure";
    private List<IEnemyAction> currentEnemyActions;
    private int actionIndex;

    public void PlanEnemyMovement(Tilemap onGroundTilemap)
    {
        if (board == null)
        {
            Debug.LogError("board null in plan enemy movement");
        }
        else
        {
            PDDLPlanner.SolveProblem(board, PDDLFileName);
            currentEnemyActions = PDDLPlanner.GetActionsFromPlan(PDDLFileName, board, onGroundTilemap);
            actionIndex = 0;
        }
    }
    
    public void ExecuteStepOfPlan()
    {
        if (actionIndex < currentEnemyActions.Count)
        {
            currentEnemyActions[actionIndex].Execute();
            actionIndex++;
        }
    }
}
