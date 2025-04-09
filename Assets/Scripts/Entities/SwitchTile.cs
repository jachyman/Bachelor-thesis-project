using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTile : ITile
{
    private Vector2Int position;
    private bool isOccupied;
    private bool isSwitchedOn;

    public Vector2Int Position { get { return position; } }
    public bool IsBlocked
    {
        get 
        {
            if (!isOccupied && isSwitchedOn)
            {
                return false;
            }
            return true;
        }
    }
    public bool IsOccupied
    {
        get { return isOccupied; }
    }
    public bool IsSwitchedOn
    {
        get => isSwitchedOn;
    }

    public SwitchTile(Vector2Int position, bool isOccupied, bool isSwitchedOn)
    {
        this.position = position;
        this.isOccupied = isOccupied;
        this.isSwitchedOn = isSwitchedOn;
    }

    public void Switch()
    {
        isSwitchedOn = !isSwitchedOn;
    }


    public void LeaveTileEffect()
    {
        isOccupied = false;
        if (isSwitchedOn)
        {
            Switch();
        }
    }

    public void EnterTileEffect(Enemy enemy)
    {
        isOccupied = true;
    }
}
