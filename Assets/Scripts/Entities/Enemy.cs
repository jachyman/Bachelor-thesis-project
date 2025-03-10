using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy
{
    private Vector2Int position;
    private int id;
    private Board board;
    private TileBase tileBase;

    public Vector2Int Position {  get { return position; } }
    public int Id { get { return id; } }
    public TileBase TileBase { get { return tileBase; } }

    public Enemy(Vector2Int position, int id, Board board, TileBase tileBase)
    {
        this.position = position;
        this.id = id;
        this.board = board;
        this.tileBase = tileBase;
    }

    public void MoveTo(ITile targetTile)
    {
        if (!targetTile.IsBlocked)
        {
            ITile fromTile = board.GetTileAt(position);
            fromTile.IsBlocked = false;
            targetTile.IsBlocked = true;
            position = targetTile.Position;
            targetTile.TriggerEffect();
        }
        else
        {
            Debug.Log("Enemy MoveTo: tile is blocked");
        }
    }
}
