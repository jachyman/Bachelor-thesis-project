using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static PDDLHelper;

public class PDDLHelper : MonoBehaviour
{
    public class Tile
    {
        // CANT HAVE MORE THAN 26 COLUMNS
        public int Row { get; set; }
        public int Column { get; set; }
        public Tile(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
    public class Wall
    {
        public Tile tile1;
        public Tile tile2;
        public Wall(int row1, int column1, int row2, int column2)
        {
            tile1 = new Tile(row1, column1);
            tile2 = new Tile(row2, column2);
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
        public Tile startLocation;
        public List<Tile> blocked;
        public List<Wall> walls;
        public List<WallTrigger> wallTriggers;
    }

    private static string TileToNotation(Tile tile, int rows)
    {
        char letter = (char)('a' + tile.Column);
        return $"{letter}{rows - tile.Row}";
    }

    private static string pddlTemplate =

@"(define (problem {problemName})

    (:domain {domainName})
    
    (:objects
        {objects} - location
    )
    
    (:init 
        (at {startLocation})
        
        ; column connections
        {columnConnections}
        
        ; row connections
        {rowConnections}
        
        {blockedLocations}
        
        {walls}

        {wallTriggers}
    )
    
    (:goal
        {goal}
    )

)";

    public static void CreatePDDLProblemFile(string name, Board board, string domain)
    {
        string pddlContent = GeneratePDDProblem(name, board, domain);
        string filePath = $"Assets/PDDL/{name}.pddl";

        File.WriteAllText(filePath, pddlContent);
        Debug.Log($"PDDL file written to {filePath}");
    }

    private static string GeneratePDDProblem(string name, Board board, string domain)
    {
        string problemName = name;
        string domainName = domain;

        StringBuilder objects = new StringBuilder();

        for (int i = board.rows; i > 0; --i)
        {
            for (int j = 0; j < board.columns; ++j)
            {
                objects.Append($"{(char)('a' + j)}{i} ");
            }
            objects.AppendLine();
            objects.Append("        ");
        }

        string startLocation = TileToNotation(board.startLocation, board.rows);

        StringBuilder rowConnections = new StringBuilder();
        for (int i = board.rows; i >= 1; --i)
        {
            for (int j = 0; j < board.columns - 1; ++j)
            {
                char firstLetter = (char)('a' + j);
                char secondLetter = (char)('a' + j + 1);
                rowConnections.Append($"(con {firstLetter}{i} {secondLetter}{i}) ");
            }
            rowConnections.AppendLine();
            rowConnections.Append("        ");
        }

        StringBuilder columnConnections = new StringBuilder();
        for (int i = 0; i < board.columns; ++i)
        {
            char columnLetter = (char)('a' + i);
            for (int j = board.rows; j > 1; --j)
            {
                columnConnections.Append($"(con {columnLetter}{j} {columnLetter}{j - 1}) ");
            }
            columnConnections.AppendLine();
            columnConnections.Append("        ");
        }

        string blockedLocations = TileListToNotation(board.blocked, board.rows, "blocked");

        StringBuilder walls = new StringBuilder();
        if (board.walls != null)
        {
            foreach (Wall wall in board.walls)
            {
                string notation1 = TileToNotation(wall.tile1, board.rows);
                string notation2 = TileToNotation(wall.tile2, board.rows);
                walls.Append($"(wall {notation1} {notation2}) ");
            }
        }

        StringBuilder wallTriggers = new StringBuilder();
        if (board.wallTriggers != null)
        {
            foreach (WallTrigger wallTrigger in board.wallTriggers)
            {
                string trigger = TileToNotation(wallTrigger.triggerTile, board.rows);
                string wall1 = TileToNotation(wallTrigger.wall.tile1, board.rows);
                string wall2 = TileToNotation(wallTrigger.wall.tile2, board.rows);
                wallTriggers.Append($"(wall-trigger {trigger} {wall1} {wall2}) ");
            }
        }

        List<Tile> goalTiles = new List<Tile>();
        for (int i = 0; i < board.columns; ++i)
        {
            goalTiles.Add(new Tile(0, i));
        }
        string goalNotations = TileListToNotation(goalTiles,board.rows, "at");
        string goal = $"(or {goalNotations})";

        return pddlTemplate
                    .Replace("{problemName}", problemName)
                    .Replace("{domainName}", domainName)
                    .Replace("{objects}", objects.ToString())
                    .Replace("{startLocation}", startLocation)
                    .Replace("{columnConnections}", columnConnections.ToString())
                    .Replace("{rowConnections}", rowConnections.ToString())
                    .Replace("{blockedLocations}", blockedLocations)
                    .Replace("{walls}", walls.ToString())
                    .Replace("{wallTriggers}", wallTriggers.ToString())
                    .Replace("{goal}", goal);
    }

    private static string TileListToNotation(List<Tile> tiles, int rows, string name)
    {
        StringBuilder listNotation = new StringBuilder();
        if (tiles != null)
        {
            foreach (Tile tile in tiles)
            {
                string notation = TileToNotation(tile, rows);
                listNotation.Append($"({name} {notation}) ");
            }
        }

        return listNotation.ToString();
    }
}
