using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using static GameManager;

public class PDDLHelper : MonoBehaviour
{
    public static void SolveProblem(Board board, string name)
    {
        //string name = "two_enemies";
        string problemName = name + "_problem";
        string domainName = name + "_domain";

        CreatePDDLProblemFile(problemName, board, domainName);

        string planName = name + "_plan";
        FastDownwardIntegration.RunFastDownward(problemName, domainName, planName);
    }
    private static string TileToNotation(Tile tile, int rows)
    {
        char letter = (char)('a' + tile.col);
        return $"{letter}{rows - tile.row}";
    }

    private static string pddlProblemTemplate =

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
        List<Tile> blockedTiles = new List<Tile>();
        StringBuilder rowConnections = new StringBuilder();
        StringBuilder columnConnections = new StringBuilder();

        for (int row = 0; row < board.rows; ++row)
        {
            int rowNumber = board.rows - row;

            for (int col = 0; col < board.columns; ++col)
            {
                char colLetter = (char)('a' + col);

                // objects
                objects.Append($"{colLetter}{rowNumber} ");

                // blocked tiles
                Tile tile = board.tiles[row, col];
                if (tile.type == TileType.Blocked)
                {
                    blockedTiles.Add(tile);
                }

                // row connections
                if (col <  board.columns - 1)
                {
                    rowConnections.Append($"(con {colLetter}{rowNumber} {(char)(colLetter + 1)}{rowNumber}) ");
                }

                //column connections
                if (row < board.rows - 1)
                {
                    columnConnections.Append($"(con {colLetter}{rowNumber} {colLetter}{rowNumber - 1}) ");
                }
            }

            objects.AppendLine();
            objects.Append("        ");
            rowConnections.AppendLine();
            rowConnections.Append("        ");
            columnConnections.AppendLine();
            columnConnections.Append("        ");
        }

        string blockedLocations = TileListToNotation(blockedTiles, board.rows, "blocked");
        string startLocation = TileToNotation(board.startLocation, board.rows);

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

        /*
        for (int i = board.rows; i > 0; --i)
        {
            for (int j = 0; j < board.columns; ++j)
            {
                objects.Append($"{(char)('a' + j)}{i} ");
            }
            objects.AppendLine();
            objects.Append("        ");
        }
        */

        /*
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
        */

        /*
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
        */


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
            goalTiles.Add(board.tiles[0,i]);
            //goalTiles.Add(new Tile(TileType.Empty,0, i));
        }

        string goalNotations = TileListToNotation(goalTiles,board.rows, "at");
        string goal = $"(or {goalNotations})";

        return pddlProblemTemplate
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
