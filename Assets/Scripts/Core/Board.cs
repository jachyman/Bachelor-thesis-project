using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private TilemapReader tilemapReader;

    public ITile[,] tiles;
    public List<Enemy> enemies;
    public List<Wall> walls;


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
        return tiles.GetLength(0);
    }
    public int GetCols()
    {
        return tiles.GetLength(1);
    }

    public Board(ITile[,] tiles, List<Enemy> enemies, List<Wall> walls)
    {
        this.tiles = tiles;
        this.enemies = enemies;
        this.walls = walls;
    }
}
