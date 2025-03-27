using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GameManager;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private Board board;

    private DomainType domainType;
    private const string PDDLFileName = "enemy";
    private List<IEnemyAction> currentEnemyActions;
    private int actionIndex;

    public enum DomainType
    {
        NoWallTriggers,
        NonSimMovement,
        SimMovement
    }

    public bool PlanEnemyMovement(UIManager uiManager, EnemyMovement enemyMovement)
    {
        if (board == null)
        {
            Debug.LogError("board null in plan enemy movement");
            return false;
        }
        else
        {
            domainType = enemyMovement == EnemyMovement.Simultanious ? DomainType.SimMovement : DomainType.NoWallTriggers;
            string domainName;
            switch (domainType)
            {
                case DomainType.NoWallTriggers:
                    domainName = "no_wall_triggers";
                    break;
                case DomainType.NonSimMovement:
                    domainName = "wall_triggers";
                    break;
                case DomainType.SimMovement:
                    domainName = "sim_movement";
                    break;
                default:
                    Debug.LogError("no match with domain type");
                    domainName = "";
                    break;
            }
            bool IsProblemSolved = PDDLPlanner.SolveProblem(board, PDDLFileName, domainName, domainType);
            if (IsProblemSolved)
            {
                currentEnemyActions = PDDLPlanner.GetActionsFromPlan(PDDLFileName, board, uiManager);
                actionIndex = 0;
                return true;
            }
            else
            {
                return false;
            }
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
