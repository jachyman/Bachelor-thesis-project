using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private TilemapReader tilemapReader;

    public ITile[,] tiles;
    public List<Enemy> enemies;
    public List<Wall> walls;
    public List<SwitchTile> switchTiles;

    private void Awake()
    {
        if (tilemapReader == null)
        {
            Debug.LogError("No tilemap reference");
        }
        else
        {
            Initilize();
        }
    }

    private void Initilize()
    {
        tilemapReader.InitilizeBoardFromTilemaps(this);
        if (tiles == null)
        {
            Debug.LogError("tiles null in INIT");
        }
        InitSwitchTiles();
    }

    private void InitSwitchTiles()
    {
        switchTiles = new List<SwitchTile>();
        foreach (ITile tile in tiles)
        {
            if (tile is SwitchTile switchTile)
            {
                switchTiles.Add(switchTile);
            }
        }
    }

    public void SwitchSwitchTiles(UIManager uIManager)
    {
        foreach (SwitchTile tile in switchTiles)
        {
            if (!tile.IsOccupied)
            {
                tile.Switch();
                uIManager.SwitchSwitchTile(tile);
            }
            else
            {
                Debug.Log("switch tile is occupied");
            }
        }
    }

    public Vector2Int TilemapToMatrixPosition(Vector2Int tilemapPosition)
    {
        int matrixX = GetRows() - 1 - tilemapPosition.y;
        int matrixY = tilemapPosition.x;
        return new Vector2Int(matrixX, matrixY);
    }

    public ITile GetTileAt(Vector2Int position)
    {
        Vector2Int matrixPosition = TilemapToMatrixPosition(position);
        return GetTileAtMatrixCoord(matrixPosition);
    }

    public ITile GetTileAtMatrixCoord(Vector2Int coord)
    {
        if (coord.x >= 0 && coord.x < GetRows() && coord.y >= 0 && coord.y < GetCols())
        {
            return tiles[coord.x, coord.y];
        }
        Debug.Log("GetTileAt: position out of bounds");
        return null;
    }

    public int GetRows()
    {
        if (tiles == null)
        {
            Debug.Log("tiles is null");
        }
        return tiles.GetLength(0);
    }
    public int GetCols()
    {
        return tiles.GetLength(1);
    }

    public bool CanBuildWallHere(Wall wall)
    {
        return (!walls.Contains(wall));
    }

    public void AddWall(Wall wall)
    {
        walls.Add(wall);
    }
    public void RemoveWall(Wall wall)
    {
        walls.Remove(wall);
    }

    public int GetAliveEnemyCount()
    {
        int count = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive)
            {
                count++;
            }
        }
        return count;
    }

    public Enemy GetNextAliveEnemy(Enemy enemy)
    {
        return GetNextAliveEnemyRec(enemy.Id);
    }

    private Enemy GetNextAliveEnemyRec(int id)
    {
        int nextId = id >= (enemies.Count) ? 1 : (id + 1);
        Enemy nextEnemy = enemies.FirstOrDefault(e => e.Id == nextId);
        if (nextEnemy == null)
        {
            Debug.LogError("next enemy not found");
            return null;
        }
        
        if (nextEnemy.IsAlive)
        {
            return nextEnemy;
        }
        else
        {
            return GetNextAliveEnemyRec(nextEnemy.Id);
        }
    }

    public Enemy GetFirstAliveEnemy()
    {
        if (IsAnyEnemyAlive())
        {
            return GetNextAliveEnemyRec(enemies.Count);
        }
        else
        {
            Debug.Log("no enemies left");
            return null;
        }
    }

    public bool IsAnyEnemyAlive()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive)
            {
                return true;
            }
        }
        return false;
    }
}
