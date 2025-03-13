using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class Enemy
{
    private Vector2Int position;
    private int id;
    private Board board;
    private TileBase tileBase;
    private GameManager gameManager;

    public Vector2Int Position {  get { return position; } }
    public int Id { get { return id; } }
    public TileBase TileBase { get { return tileBase; } }

    public Enemy(Vector2Int position, int id, Board board, TileBase tileBase, GameManager gameManager)
    {
        this.position = position;
        this.id = id;
        this.board = board;
        this.tileBase = tileBase;
        this.gameManager = gameManager;
    }

    public void MoveTo(ITile targetTile, Tilemap tilemap)
    {
        if (!targetTile.IsBlocked)
        {
            Vector3Int tilemapFromCoord = new Vector3Int(position.x, position.y, 0);
            Vector3Int tilemapToCoord = new Vector3Int(targetTile.Position.x, targetTile.Position.y, 0);
            tilemap.SetTile(tilemapToCoord, TileBase);
            tilemap.SetTile(tilemapFromCoord, null);

            ITile fromTile = board.GetTileAt(position);
            fromTile.IsBlocked = false;
            targetTile.IsBlocked = true;
            position = targetTile.Position;

            targetTile.TriggerEffect();
            gameManager.CheckEndConditions();
        }
        else
        {
            Debug.Log("Enemy MoveTo: tile is blocked");
        }
    }
}
