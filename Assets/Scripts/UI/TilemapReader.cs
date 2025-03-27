using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapReader : MonoBehaviour
{
    const string emptyTileName = "tile_empty";
    const string blockedTileName = "tile_blocked";
    const string trapTileName = "tile_trap";
    const string wallTriggerTileName = "wall_trigger";
    const string enemyTileName = "enemy";
    const string goalTileName = "tile_goal";

    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap horizonatalWallTilemap;
    [SerializeField] private Tilemap verticalWallTilemap;
    [SerializeField] private Tilemap onGroundTilemap;

    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public Tilemap GroundTilemap { get { return groundTilemap; } }
    public Tilemap HorizonatalWallTilemap { get { return horizonatalWallTilemap; } }
    public Tilemap VerticallWallTilemap { get { return verticalWallTilemap; } }
    public Tilemap OnGroundTilemap {  get { return onGroundTilemap; } }

    private Dictionary<int, Wall> indexWallDict;

    public void InitilizeBoardFromTilemaps(Board board)
    {
        board.walls = GetWallsFromWallTilemaps();
        board.tiles = GetTilesFromGroundTilemap(board);
        board.enemies = GetEnemiesFromTilemap(board);

        if (board.tiles == null)
        {
            Debug.LogError("tiles null in tile reader");
        }
    }

    private ITile[,] GetTilesFromGroundTilemap(Board board)
    {
        ITile[,] tiles = new ITile[rows, columns];

        for (int x = 0; x < columns; ++x)
        {
            for (int y = 0; y < rows; ++y)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = groundTilemap.GetTile(position);

                string tileName = tile.name;
                string[] tileNameSplit = tileName.Split('_');
                tileName = tileNameSplit[0] + "_" + tileNameSplit[1];
                int id = -1;
                if (tileNameSplit.Length == 3)
                {
                    id = int.Parse(tileNameSplit[2]);
                }
                Vector2Int tilePosition = new Vector2Int(x, y);

                int row = rows - y - 1;
                int col = x;

                switch (tileName)
                {
                    case emptyTileName:
                        tiles[row, col] = new BaseTile(tilePosition, false, false);
                        break;
                    case blockedTileName:
                        tiles[row, col] = new BaseTile(tilePosition, true, false);
                        break;
                    case goalTileName:
                        tiles[row, col] = new BaseTile(tilePosition, false, true);
                        break;
                    case trapTileName:
                        tiles[row, col] = new TrapTile(tilePosition, OnGroundTilemap);
                        break;
                    case wallTriggerTileName:
                        Wall wall = indexWallDict[id];
                        WallTriggerTile wallTrigger = new WallTriggerTile(
                            tilePosition,
                            false,
                            wall,
                            false
                        );
                        tiles[row, col] = wallTrigger;
                        break;
                    default:
                        Debug.LogError("tile string not compatable : " + tileName);
                        break;
                }
            }
        }
        return tiles;
    }

    public List<Wall> GetWallsFromWallTilemaps()
    {
        List<Wall> walls = new List<Wall>();
        indexWallDict = new Dictionary<int, Wall>();

        for (int x = 0; x < columns; ++x)
        {
            for (int y = 0; y < rows; ++y)
            {
                Vector3Int tilemapPosition = new Vector3Int(x, y, 0);
                TileBase horizonatlTile = horizonatalWallTilemap.GetTile(tilemapPosition);
                TileBase verticalTile = verticalWallTilemap.GetTile(tilemapPosition);

                Vector2Int position = new Vector2Int(x, y);

                if (horizonatlTile != null)
                {
                    Wall wall = new Wall(position, true, horizonatlTile, horizonatalWallTilemap);
                    walls.Add(wall);

                    AddWallToTrigger(horizonatlTile.name, wall);
                }
                if (verticalTile != null)
                {
                    Wall wall = new Wall(position, false, verticalTile, verticalWallTilemap);
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
            indexWallDict[wallTriggerId] = wall;
        }
    }

    private List<Enemy> GetEnemiesFromTilemap(Board board)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int y = rows - 1; y >= 0; y--)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3Int tilemapPosition = new Vector3Int(x, y, 0);
                TileBase tile = onGroundTilemap.GetTile(tilemapPosition);
                Vector2Int position = new Vector2Int(x, y);

                if (tile != null)
                {
                    string tileName = tile.name;
                    string[] tileNameSplit = tileName.Split('_');
                    if (tileNameSplit[0] == enemyTileName)
                    {
                        int enemyId = int.Parse(tileNameSplit[1]);
                        Enemy enemy = new Enemy(position, enemyId, board, tile, gameManager, uiManager);
                        enemies.Add(enemy);
                    }
                }
            }
        }
        return enemies;
    }

}
