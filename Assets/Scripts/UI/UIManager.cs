using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TilemapReader tilemapReader;
    [SerializeField] private Board board;

    public void MoveEnemyToTile(Enemy enemy, ITile tile)
    {
        Vector3Int from = new Vector3Int(enemy.Position.x, enemy.Position.y, 0);
        Vector3Int to = new Vector3Int(tile.Position.x, tile.Position.y, 0);

        tilemapReader.OnGroundTilemap.SetTile(to, enemy.TileBase);
        tilemapReader.OnGroundTilemap.SetTile(from, null);
    }

    public Tilemap GetOnGroundTilemap()
    {
        return tilemapReader.OnGroundTilemap;
    }

    public void WallSetActive(Wall wall, bool active)
    {
        Tilemap tilemap;
        if (wall.IsHorizontal)
        {
            tilemap = tilemapReader.HorizonatalWallTilemap;
        }
        else
        {
            tilemap = tilemapReader.VerticallWallTilemap;
        }

        Vector3Int tilemapPosition = new Vector3Int(wall.Position.x, wall.Position.y, 0);
        tilemap.SetTile(tilemapPosition, active ? wall.TileBase : null);
    }
}