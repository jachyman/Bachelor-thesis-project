using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static GameManager;

public class UIManager : MonoBehaviour
{
    const string emptyTileName = "tile_empty";
    const string blockedTileName = "tile_blocked";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                        tiles[tileRow, tileCol] = new GameManager.Tile(TileType.Empty, tileRow, tileCol);
                        break;
                    case blockedTileName:
                        tiles[tileRow, tileCol] = new GameManager.Tile(TileType.Blocked, tileRow, tileCol);
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
}
