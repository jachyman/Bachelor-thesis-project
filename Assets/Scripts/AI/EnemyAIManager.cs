using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private Board board;

    private const string PDDLFileName = "new_structure";

    public void PlanEnemyMovement()
    {
        if (board == null)
        {
            Debug.LogError("board null in plan enemy movement");
        }
        else
        {
            PDDLPlanner.SolveProblem(board, PDDLFileName);
        }
    }
    
    public void ExecuteStepOfPlan()
    {

    }
}
