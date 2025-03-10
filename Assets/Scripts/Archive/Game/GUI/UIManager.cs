using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

using static GameManager;

/*
public class UIManager : MonoBehaviour
{
    const string emptyTileName = "tile_empty";
    const string blockedTileName = "tile_blocked";
    const string wallTriggerTileName = "wall_trigger";
    const string enemyTileName = "enemy";

    private Tilemap groundTilemap;
    private Tilemap horizonatalWallTilemap;
    private Tilemap verticalTilemap;
    private Tilemap onGroundTilemap;
    private int rows;
    private int columns;

    public UIManager(Tilemap groundTilemap, Tilemap horizonatalWallTilemap, Tilemap verticalTilemap, Tilemap onGroundTilemap, int rows, int columns)
    {
        this.groundTilemap = groundTilemap;
        this.horizonatalWallTilemap = horizonatalWallTilemap;
        this.verticalTilemap = verticalTilemap;
        this.onGroundTilemap = onGroundTilemap;
        this.rows = rows;
        this.columns = columns;
    }

    public List<WallTrigger> wallTriggers = new List<WallTrigger>();

    public Board GetBoardFromTilemaps()
    {
        GameManager.Tile[,] tiles = GetTilesFromGroundTilemap();
        List<Wall> walls = GetWallsFromWallTilemaps(tiles);
        List<Enemy> enemies = GetEnemiesFromTilemap(tiles);

        Board board = new Board();

        board.rows = rows;
        board.columns = columns;
        board.tiles = tiles;
        board.enemies = enemies;
        board.walls = walls;
        board.wallTriggers = wallTriggers;

        return board;
    }

    private List<Enemy> GetEnemiesFromTilemap(GameManager.Tile[,] groundTiles)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = onGroundTilemap.GetTile(position);

                if (tile != null)
                {
                    int tileRow = rows - y - 1;
                    int tileCol = x;

                    switch (tile.name)
                    {
                        case enemyTileName:
                            Enemy enemy = new Enemy(groundTiles[tileRow, tileCol], tile);
                            enemies.Add(enemy);
                            break;
                    }
                }
            }
        }
        return enemies;
    }

    Dictionary<int, WallTrigger> indexWallTrigger;
    public GameManager.Tile[,] GetTilesFromGroundTilemap()
    {
        wallTriggers = new List<WallTrigger>();
        GameManager.Tile[,] tiles = new GameManager.Tile[rows, columns];
        indexWallTrigger = new Dictionary<int, WallTrigger>();

        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = groundTilemap.GetTile(position);

                //Debug.Log((y - rows + 1) + " " + x);
                int tileRow = rows - y - 1;
                int tileCol = x;

                string tileName = tile.name;
                string[] tileNameSplit = tileName.Split('_');
                tileName = tileNameSplit[0] + "_"  + tileNameSplit[1];
                int id = -1;
                if (tileNameSplit.Length == 3)
                {
                    id = int.Parse(tileNameSplit[2]);
                }

                switch (tileName)
                {
                    case emptyTileName:
                        tiles[tileRow, tileCol] = new GameManager.Tile(false, tileRow, tileCol, position);
                        break;
                    case blockedTileName:
                        tiles[tileRow, tileCol] = new GameManager.Tile(true, tileRow, tileCol, position);
                        break;
                    case wallTriggerTileName:
                        WallTrigger wallTrigger = new WallTrigger(false, tileRow, tileCol, position, null, false, id);
                        tiles[tileRow, tileCol] = wallTrigger;
                        indexWallTrigger[id] = wallTrigger;
                        wallTriggers.Add(wallTrigger);
                        break;
                    default:
                        Debug.Log("tile string not compatable");
                        break;
                }
            }
        }
        return tiles;
    }

    public List<Wall> GetWallsFromWallTilemaps(GameManager.Tile[,] groundTiles)
    {
        List<Wall> walls = new List<Wall>();

        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase horizonatlTile = horizonatalWallTilemap.GetTile(position);
                TileBase verticalTile = verticalTilemap.GetTile(position);

                int tileRow = rows - y - 1;
                int tileCol = x;

                if (horizonatlTile != null && tileRow < rows - 1)
                {
                    Wall wall = new Wall(groundTiles[tileRow, tileCol], groundTiles[tileRow + 1, tileCol], position, true, horizonatlTile);
                    walls.Add(wall);

                    AddWallToTrigger(horizonatlTile.name, wall);
                }
                if (verticalTile != null && tileCol > 0)
                {
                    Wall wall = new Wall(groundTiles[tileRow, tileCol], groundTiles[tileRow, tileCol - 1], position, false, verticalTile);
                    walls.Add(wall);

                    AddWallToTrigger(verticalTile.name, wall);
                }
            }
        }
        return walls;
    }

    private void AddWallToTrigger(string name, Wall wall)
    {
        int wallTriggerId = -1;
        string[] split = name.Split('_');
        if (split.Length == 3)
        {
            wallTriggerId = int.Parse(split[2]);
            WallTrigger wallTrigger = indexWallTrigger[wallTriggerId];
            if (wallTrigger != null)
            {
                wallTrigger.wall = wall;
            }
        }
    }

    public static void MoveEnemyToTile(Enemy enemy, GameManager.Tile tile, Tilemap onGroundTilemap)
    {
        Vector3Int from = enemy.tilePosition.position;
        Vector3Int to = tile.position;

        onGroundTilemap.SetTile(to, enemy.tileBase);
        onGroundTilemap.SetTile(from, null);
    }

    public void WallSetActive(Wall wall, bool active)
    {
        Tilemap tilemap;
        if (wall.horizontal)
        {
            tilemap = horizonatalWallTilemap;
        }
        else
        {
            tilemap = verticalTilemap;
        }

        tilemap.SetTile(wall.position, active ? wall.tileBase : null);
    }
}
*/
