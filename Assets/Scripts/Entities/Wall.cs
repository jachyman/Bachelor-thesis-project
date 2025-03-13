using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall
{
    private Vector2Int position;
    private bool isActive = true;
    private bool isHorizontal;
    private TileBase tileBase;
    private Tilemap tilemap;

    public Vector2Int Position => position;
    public bool IsActive => isActive;
    public bool IsHorizontal => isHorizontal;
    public TileBase TileBase {  get { return tileBase; } }

    public Wall(Vector2Int positon, bool isHorizontal, TileBase tileBase, Tilemap tilemap)
    {
        this.position = positon;
        this.isHorizontal = isHorizontal;
        this.tileBase = tileBase;
        this.tilemap = tilemap;
    }

    public void SetActive(bool active)
    {
        this.isActive = active;
        Vector3Int tilemapCoord = new Vector3Int(position.x, position.y, 0);
        tilemap.SetTile(tilemapCoord, active ? TileBase : null);
    }

    public override bool Equals(object obj)
    {
        return obj is Wall wall &&
               position.Equals(wall.position) &&
               isHorizontal == wall.isHorizontal;
    }
}
