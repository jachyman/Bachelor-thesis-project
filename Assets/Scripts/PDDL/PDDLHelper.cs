using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using static GameManager;

public class PDDLHelper : MonoBehaviour
{
    private const string PDDLPath = "Assets/PDDL/";

    private const string moveActionString = "move";

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
    private static Tile NotationToTile(string notation, Board board)
    {
        char letter = notation[0];
        int number = (int)Char.GetNumericValue(notation[1]);

        int row = board.rows - number;
        int col = letter - 'a';

        return board.tiles[row, col];
    }
    private static Enemy NotationToEnemy(string notation, Board board)
    {
        foreach (Enemy enemy in board.enemies)
        {
            if (enemy.PDDLNotation == notation)
            {
                return enemy;
            }
        }

        Debug.Log("Reading plan: enemy not found");
        return null;
    }

    private static string pddlProblemTemplate =

@"(define (problem {problemName})

    (:domain {domainName})
    
    (:objects
        {locations} - location
        {enemies} - enemy
    )
    
    (:init 
        {enemiesStartLocation}
        
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

        StringBuilder locations = new StringBuilder();
        List<Tile> blockedTiles = new List<Tile>();
        StringBuilder rowConnections = new StringBuilder();
        StringBuilder columnConnections = new StringBuilder();

        for (int row = 0; row < board.rows; ++row)
        {
            int rowNumber = board.rows - row;

            for (int col = 0; col < board.columns; ++col)
            {
                char colLetter = (char)('a' + col);

                // locations
                locations.Append($"{colLetter}{rowNumber} ");

                // blocked tiles
                Tile tile = board.tiles[row, col];
                if (tile.blocked)
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

            locations.AppendLine();
            locations.Append("        ");
            rowConnections.AppendLine();
            rowConnections.Append("        ");
            columnConnections.AppendLine();
            columnConnections.Append("        ");
        }

        string blockedLocations = TileListToNotation(blockedTiles, board.rows, "blocked");

        // enemies
        StringBuilder enemies = new StringBuilder();
        int enemyIndex = 0;
        foreach (Enemy enemy in board.enemies)
        {
            string enemyNotation = "en" + enemyIndex;
            enemies.Append(enemyNotation + " ");
            enemy.PDDLNotation = enemyNotation;
            enemyIndex++;
        }

        // enemies start location
        StringBuilder enemiesStartLocation = new StringBuilder();
        foreach (Enemy enemy in board.enemies)
        {
            string startLocation = TileToNotation(enemy.tilePosition, board.rows);
            enemiesStartLocation.Append("(enemy_loc " + enemy.PDDLNotation + " " + startLocation + ")");
            enemiesStartLocation.AppendLine();
            enemiesStartLocation.Append("        ");
            enemyIndex++;
        }

        // walls
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

        // wall triggers
        StringBuilder wallTriggers = new StringBuilder();
        if (board.wallTriggers != null)
        {
            foreach (WallTrigger wallTrigger in board.wallTriggers)
            {
                string trigger = TileToNotation(wallTrigger, board.rows);
                string wall1 = TileToNotation(wallTrigger.wall.tile1, board.rows);
                string wall2 = TileToNotation(wallTrigger.wall.tile2, board.rows);
                wallTriggers.Append($"(reverse-wall-trigger {trigger} {wall1} {wall2}) ");
            }
        }

        List<Tile> goalTiles = new List<Tile>();
        for (int i = 0; i < board.columns; ++i)
        {
            goalTiles.Add(board.tiles[0,i]);
            //goalTiles.Add(new Tile(TileType.Empty,0, i));
        }

        //string goalNotations = TileListToNotation(goalTiles, board.rows, "at");
        StringBuilder goalNotations = new StringBuilder();
        foreach (Enemy enemy in board.enemies)
        {
            foreach (Tile tile in goalTiles)
            {
                goalNotations.Append("(enemy_loc " + enemy.PDDLNotation + " ");
                goalNotations.Append(TileToNotation(tile, board.rows));
                goalNotations.Append(") ");
            }
            goalNotations.AppendLine();
            goalNotations.Append("          ");
        }
        string goal =
            $"(or\n" +
            $"          {goalNotations})";

        return pddlProblemTemplate
                    .Replace("{problemName}", problemName)
                    .Replace("{domainName}", domainName)
                    .Replace("{locations}", locations.ToString())
                    .Replace("{enemies}", enemies.ToString())
                    .Replace("{enemiesStartLocation}", enemiesStartLocation.ToString())
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

    public static List<GameManager.Action> GetActionsFromPlan(string planName, Board board)
    {
        string path = PDDLPath + planName;
        List<GameManager.Action> actions = new List<GameManager.Action>();

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[0] != ';')
                    {
                        line = line.Replace("(", "").Replace(")", "") ;

                        string[] subs = line.Split(' ');
                        string action = subs[0];
                        string[] arguments = new string[subs.Length - 1];
                        Array.Copy(subs, 1, arguments, 0, arguments.Length);

                        switch (action)
                        {
                            case moveActionString:
                                Tile toTile = NotationToTile(arguments[1], board);
                                Enemy enemy = NotationToEnemy(arguments[2], board);
                                MoveAction moveAction = new MoveAction(enemy, toTile);
                                actions.Add(moveAction);
                                break;
                            default:
                                Debug.Log("GetActionsFromPlan: action not found");
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        return actions;
    }
}
