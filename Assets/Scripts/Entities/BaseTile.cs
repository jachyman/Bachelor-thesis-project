using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : ITile
{
    private Vector2Int position;
    private bool isBlocked;
    private bool isOccupied;
    private bool isGoal;

    public Vector2Int Position {  get { return position; } }
    public bool IsBlocked {
        get { return isBlocked || isOccupied; } 
    }
    public bool IsGoal { get { return isGoal; } }


    public BaseTile (Vector2Int position, bool isBlocked, bool isGoal)
    {
        this.position = position;
        this.isBlocked = isBlocked;
        this.isGoal = isGoal;
        isOccupied = false;
    }

    public void EnterTileEffect(Enemy enemy)
    {
        isOccupied = true;
    }

    public void LeaveTileEffect()
    {
        isOccupied = false;
    }
}
