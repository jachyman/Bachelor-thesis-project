using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy
{
    private Vector2Int position;
    private int id;
    private TileBase tileBase;
    private bool isAlive;

    public Vector2Int Position {  get { return position; } }
    public int Id { get { return id; } }
    public TileBase TileBase { get { return tileBase; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

    public Enemy(Vector2Int position, int id, TileBase tileBase)
    {
        this.position = position;
        this.id = id;
        this.tileBase = tileBase;
        isAlive = true;
    }

    public void MoveTo(ITile targetTile)
    {
        if (!targetTile.IsBlocked)
        {
            position = targetTile.Position;
            targetTile.TriggerEffect(this);
        }
        else
        {
            Debug.Log("Enemy MoveTo: tile is blocked");
        }
    }

    public void Kill()
    {
        IsAlive = false;
    }
}
