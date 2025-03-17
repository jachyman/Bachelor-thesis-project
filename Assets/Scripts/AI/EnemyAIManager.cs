using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private DomainType domainType;
    [SerializeField] private Board board;


    private const string PDDLFileName = "enemy";
    private List<IEnemyAction> currentEnemyActions;
    private int actionIndex;

    private enum DomainType
    {
        NoWallTriggers,
        WallTriggers
    }

    public void PlanEnemyMovement(Tilemap onGroundTilemap)
    {
        if (board == null)
        {
            Debug.LogError("board null in plan enemy movement");
        }
        else
        {
            string domainName;
            switch (domainType)
            {
                case DomainType.NoWallTriggers:
                    domainName = "no_wall_triggers";
                    break;
                case DomainType.WallTriggers:
                    domainName = "wall_triggers";
                    break;
                default:
                    Debug.LogError("no match with domain type");
                    domainName = "";
                    break;
            }
            PDDLPlanner.SolveProblem(board, PDDLFileName, domainName);
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
