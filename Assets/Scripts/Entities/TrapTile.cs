using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapTile : ITile
{
    private Vector2Int position;
    private bool isBlocked;
    private Tilemap onGroundTilemap;

    public Vector2Int Position => position;
    public bool IsBlocked
    {
        get { return isBlocked; }
        set { isBlocked = value; }
    }
    public TrapTile(Vector2Int position, Tilemap onGroundTilemap)
    {
        this.position = position;
        isBlocked = false;
        this.onGroundTilemap = onGroundTilemap;
    }

    public void TriggerEffect(Enemy enemy)
    {
        enemy.Kill(onGroundTilemap);
        IsBlocked = true;
    }
}
