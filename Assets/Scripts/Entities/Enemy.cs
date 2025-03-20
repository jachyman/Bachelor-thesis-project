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
    private GameManager gameManager;
    private UIManager uiManager;
    private bool isAlive;

    public Vector2Int Position {  get { return position; } }
    public int Id { get { return id; } }
    public TileBase TileBase { get { return tileBase; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

    public Enemy(Vector2Int position, int id, Board board, TileBase tileBase, GameManager gameManager, UIManager uiManager)
    {
        this.position = position;
        this.id = id;
        this.board = board;
        this.tileBase = tileBase;
        this.gameManager = gameManager;
        this.uiManager = uiManager;
        isAlive = true;
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
            //fromTile.IsBlocked = false;
            //targetTile.IsBlocked = true;
            position = targetTile.Position;

            targetTile.TriggerEffect(this);
            gameManager.CheckEndConditions();
        }
        else
        {
            Debug.Log("Enemy MoveTo: tile is blocked");
        }
    }

    public void Kill(Tilemap OnGroundTilemap)
    {
        IsAlive = false;
        Vector3Int tilemapPosition = new Vector3Int(position.x, position.y, 0);
        OnGroundTilemap.SetTile(tilemapPosition, uiManager.deadEnemyTileBase);
    }
}
