using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTriggerTile : ITile
{
    private Vector2Int position;
    private bool isBlocked;
    private Wall wall;
    private bool setWall;

    public Vector2Int Position { get { return position; } }
    public Wall Wall { get { return wall; } }

    public bool IsBlocked
    {
        get { return isBlocked; }
        set { isBlocked = value; }
    }

    public WallTriggerTile(Vector2Int position, bool isBlocked, Wall wall, bool setWall)
    {
        this.position = position;
        this.isBlocked = isBlocked;
        this.wall = wall;
        this.setWall = setWall;
    }

    public void TriggerEffect(Enemy enemy)
    {
        wall.SetActive(setWall);
    }
}
