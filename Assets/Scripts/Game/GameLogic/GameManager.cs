using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static UIManager;
using static PDDLHelper;

public class GameManager : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap horizontalWallsTilemap;
    public Tilemap verticalWallsTilemap;

    public int rows;
    public int columns;    

    public enum TileType
    {
        Empty,
        Blocked
    }
    public class Tile
    {
        // CANT HAVE MORE THAN 26 COLUMNS

        public TileType type;
        public int row;
        public int col;

        public Tile(TileType type, int row, int col)
        {
            this.type = type;
            this.row = row;
            this.col = col;
        }

        /*
        public int Row { get; set; }
        public int Column { get; set; }
        public Tile(int row, int column)
        {
            Row = row;
            Column = column;
        }
        */
    }
    public class Wall
    {
        public Tile tile1;
        public Tile tile2;
        public Wall(Tile tile1, Tile tile2)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
        }
    }
    public class WallTrigger
    {
        public Tile triggerTile;
        public Wall wall;
        public WallTrigger(Tile triggerTile, Wall wall)
        {
            this.triggerTile = triggerTile;
            this.wall = wall;
        }
    }
    public class Board
    {
        public int rows;
        public int columns;
        public Tile[,] tiles;
        public Tile startLocation;
        public List<Wall> walls;
        public List<WallTrigger> wallTriggers;
    }

    Board basic_board;
    Board complex_board;

    /*
    private void InitBoards()
    {
        basic_board = new Board();
        basic_board.columns = 6;
        basic_board.rows = 6;
        basic_board.startLocation = new Tile(5, 3);
        basic_board.blocked = new List<Tile>()
        {
            new Tile(4, 3),
            new Tile(4, 4),
            new Tile(4, 2),
            new Tile(4, 1),
        };
        basic_board.walls = new List<Wall>()
        {
            new Wall(1, 1, 2, 1),
            new Wall(1, 1, 1, 2),
        };
        basic_board.wallTriggers = new List<WallTrigger>()
        {
            new WallTrigger(new Tile(5, 5), new Wall(4, 5, 3, 5))
        };


        complex_board = new Board();
        complex_board.columns = 8;
        complex_board.rows = 8;
        complex_board.startLocation = new Tile(7, 4);
        complex_board.blocked = new List<Tile>()
        {
            new Tile(1, 0),
            new Tile(1, 1),
            new Tile(1, 2),
            new Tile(1, 3),
            new Tile(2, 3),
            new Tile(3, 1),
            new Tile(3, 6),
            new Tile(3, 7),
            new Tile(4, 1),
            new Tile(4, 4),
            new Tile(5, 1),
            new Tile(5, 6),
            new Tile(6, 1),
            new Tile(6, 2),
            new Tile(6, 4),
            new Tile(6, 6),
        };
        complex_board.wallTriggers = new List<WallTrigger>()
        {
            // A
            new WallTrigger(new Tile(6, 3), new Wall(3, 3, 4, 3)),
            new WallTrigger(new Tile(6, 3), new Wall(1, 5, 2, 5)),
            // B
            new WallTrigger(new Tile(6, 5), new Wall(3, 5, 4, 5)),
            new WallTrigger(new Tile(6, 5), new Wall(1, 4, 2, 4)),
            // C
            new WallTrigger(new Tile(4, 6), new Wall(2, 5, 3, 5)),
            new WallTrigger(new Tile(4, 6), new Wall(1, 4, 2, 4)),
        };
    }
    */

    void Start()
    {
        //InitBoards();
        Tile[,] tiles = GetTilesFromGroundTilemap(groundTilemap, rows, columns);
        /*
        for (int i = 0; i < 7; ++i)
        {
            for (int j = 0; j < 7; ++j)
            {
                Debug.Log(i + " " + j);
                if (tiles[i, j] == null)
                {
                    Debug.Log("NULL");
                }
                else
                {
                    Debug.Log(tiles[i, j].type == TileType.Empty ? "EMPTY" : "BLOCKED");
                }
            }
        }
        */
        List<Wall> walls = GetWallsFromWallTilemaps(horizontalWallsTilemap, verticalWallsTilemap, rows, columns, tiles);
        /*
        foreach (Wall wall in walls)
        {
            Debug.Log("wall between " + wall.tile1.row + " " + wall.tile1.col + " - " + wall.tile2.row + " " + wall.tile2.col);
        }
        */

        Board board = new Board();

        board.rows = rows;
        board.columns = columns;
        board.tiles = tiles;
        board.startLocation = tiles[rows - 1, columns / 2];
        board.walls = walls;
        board.wallTriggers = new List<WallTrigger>();

        CreatePDDLProblemFile("tilemap_generated", board, "no_domain");
    }
}
