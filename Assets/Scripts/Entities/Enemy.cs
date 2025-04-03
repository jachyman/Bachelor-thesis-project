using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy
{
    private Vector2Int position;
    private ITile currentTile;
    private int id;
    private TileBase tileBase;
    private bool isAlive;

    public Vector2Int Position {  get { return position; } }
    public int Id { get { return id; } }
    public TileBase TileBase { get { return tileBase; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

    public Enemy(Vector2Int position, int id, TileBase tileBase, ITile currentTile)
    {
        this.position = position;
        this.id = id;
        this.tileBase = tileBase;
        isAlive = true;
        this.currentTile = currentTile;
    }

    public void MoveTo(ITile targetTile)
    {
        currentTile.IsBlocked = false;
        currentTile = targetTile;
        currentTile.IsBlocked = true;
        position = targetTile.Position;
        targetTile.TriggerEffect(this);
    }

    public void Kill()
    {
        IsAlive = false;
    }
}
