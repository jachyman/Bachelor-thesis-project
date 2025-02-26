using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

using static GameManager;

public class UIManager : MonoBehaviour
{
    const string emptyTileName = "tile_empty";
    const string blockedTileName = "tile_blocked";
    const string enemyTileName = "enemy";

    public static Board GetBoardFromTilemaps(Tilemap groundTilemap, Tilemap horizonatalWallTilemap, Tilemap verticalTilemap, Tilemap onGroundTilemap, int rows, int columns)
    {
        GameManager.Tile[,] tiles = GetTilesFromGroundTilemap(groundTilemap, rows, columns);
        List<Wall> walls = GetWallsFromWallTilemaps(horizonatalWallTilemap, verticalTilemap, rows, columns, tiles);
        Enemy enemy = GetEnemyFromTilemap(onGroundTilemap, tiles, rows, columns);

        Board board = new Board();

        board.rows = rows;
        board.columns = columns;
        board.tiles = tiles;
        board.enemy = enemy;
        board.walls = walls;

        return board;
    }

    private static Enemy GetEnemyFromTilemap(Tilemap tilemap, GameManager.Tile[,] groundTiles, int rows, int columns)
    {
        Enemy enemy = null;
        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(position);

                if (tile != null)
                {
                    int tileRow = rows - y - 1;
                    int tileCol = x;

                    switch (tile.name)
                    {
                        case enemyTileName:
                            enemy = new Enemy(groundTiles[tileRow, tileCol], tile);
                            break;
                    }
                }
            }
        }
        return enemy;
    }

    public static GameManager.Tile[,] GetTilesFromGroundTilemap(Tilemap tilemap, int rows, int colums)
    {
        GameManager.Tile[,] tiles = new GameManager.Tile[rows, colums];
        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < colums; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(position);

                //Debug.Log((y - rows + 1) + " " + x);
                int tileRow = rows - y - 1;
                int tileCol = x;

                switch (tile.name)
                {
                    case emptyTileName:
                        tiles[tileRow, tileCol] = new GameManager.Tile(TileType.Empty, tileRow, tileCol, position);
                        break;
                    case blockedTileName:
                        tiles[tileRow, tileCol] = new GameManager.Tile(TileType.Blocked, tileRow, tileCol, position);
                        break;
                    default:
                        Debug.Log("tile string not compatable");
                        break;
                }
            }
        }
        return tiles;
    }

    public static List<Wall> GetWallsFromWallTilemaps(Tilemap horizonatalWallTilemap, Tilemap verticalTilemap, int rows, int colums, GameManager.Tile[,] groundTiles)
    {
        List<Wall> walls = new List<Wall>();

        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < colums; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase horizonatlTile = horizonatalWallTilemap.GetTile(position);
                TileBase verticalTile = verticalTilemap.GetTile(position);

                //Debug.Log((y - rows + 1) + " " + x);
                int tileRow = rows - y - 1;
                int tileCol = x;

                if (horizonatlTile != null && tileRow < rows - 1)
                {
                    Wall wall = new Wall(groundTiles[tileRow, tileCol], groundTiles[tileRow + 1, tileCol]);
                    walls.Add(wall);
                }
                if (verticalTile != null && tileCol > 0)
                {
                    Wall wall = new Wall(groundTiles[tileRow, tileCol], groundTiles[tileRow, tileCol - 1]);
                    walls.Add(wall);
                }
            }
        }
        return walls;
    }

    public static void MoveEnemyToTile(Enemy enemy, GameManager.Tile tile, Tilemap onGroundTilemap)
    {
        Vector3Int from = enemy.tilePosition.position;
        Vector3Int to = tile.position;

        onGroundTilemap.SetTile(to, enemy.tileBase);
        onGroundTilemap.SetTile(from, null);
    }
}
