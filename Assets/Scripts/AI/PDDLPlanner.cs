using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class PDDLPlanner : MonoBehaviour
{
    private const string PDDLPath = "Assets/PDDL/";

    private const string moveActionString = "move";
    private const string enemyNameString = "en";
    private const string enemyLocationString = "enemy_loc";

    public static void SolveProblem(Board board, string name)
    {
        string problemName = name + "_problem";
        //string domainName = name + "_domain";
        string domainName = "two_enemies_domain";

        CreatePDDLProblemFile(problemName, board, domainName);

        string planName = name + "_plan";
        FastDownwardIntegration.RunFastDownward(problemName, domainName, planName);
    }

    public static List<IEnemyAction> GetActionsFromPlan(string planName, Board board)
    {
        string path = PDDLPath + planName;
        List<IEnemyAction> actions = new List<IEnemyAction>();

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[0] != ';')
                    {
                        line = line.Replace("(", "").Replace(")", "");

                        string[] subs = line.Split(' ');
                        string action = subs[0];
                        string[] arguments = new string[subs.Length - 1];
                        Array.Copy(subs, 1, arguments, 0, arguments.Length);

                        switch (action)
                        {
                            case moveActionString:
                                ITile toTile = NotationToTile(arguments[1], board);
                                Enemy enemy = NotationToEnemy(arguments[2], board);
                                MoveAction moveAction = new MoveAction(enemy, toTile, board);
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

    private static void CreatePDDLProblemFile(string name, Board board, string domain)
    {
        string pddlContent = GeneratePDDProblem(name, board, domain);
        string filePath = $"Assets/PDDL/{name}.pddl";

        File.WriteAllText(filePath, pddlContent);
        Debug.Log($"PDDL file written to {filePath}");
    }
    
    private static string PositionToNotation(Vector2Int position)
    {
        char letter = (char)('a' + position.x);
        return $"{letter}{position.y + 1}";
    }
    private static ITile NotationToTile(string notation, Board board)
    {
        char letter = notation[0];
        int number = (int)Char.GetNumericValue(notation[1]);

        int x = letter - 'a';
        int y = number - 1;

        return board.GetTileAt(new Vector2Int(x, y));
        //return board.tiles[row, col];
    }
    private static Enemy NotationToEnemy(string notation, Board board)
    {
        foreach (Enemy enemy in board.enemies)
        {
            string enemyPDDLNotation = EnemyToNotation(enemy);
            if (enemyPDDLNotation == notation)
            {
                return enemy;
            }
        }

        Debug.Log("Reading plan: enemy not found");
        return null;
    }
    private static string EnemyToNotation(Enemy enemy)
    {
        return (enemyNameString + enemy.Id);
    }
    private static string WallToNotation(Wall wall)
    {
        string notation = "";
        if (wall.IsHorizontal)
        {
            if (wall.Position.y > 0)
            {
                string notation1 = PositionToNotation(wall.Position);
                string notation2 = PositionToNotation(new Vector2Int(wall.Position.x, wall.Position.y - 1));
                notation = $"{notation1} {notation2}";
            }
            else
            {
                Debug.Log("Wall to notation: Horizontal Wall out of bounds");
            }
        }
        else
        {
            if (wall.Position.x > 0)
            {
                string notation1 = PositionToNotation(wall.Position);
                string notation2 = PositionToNotation(new Vector2Int(wall.Position.x - 1, wall.Position.y));
                notation = $"{notation1} {notation2}";
            }
            else
            {
                Debug.Log("Wall to notation: Vertical Wall out of bounds");
            }
        }

        return notation;
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

    private static string GeneratePDDProblem(string name, Board board, string domain)
    {
        string problemName = name;
        string domainName = domain;

        StringBuilder locations = new StringBuilder();
        StringBuilder rowConnections = new StringBuilder();
        StringBuilder columnConnections = new StringBuilder();
        StringBuilder blockedTiles = new StringBuilder();
        StringBuilder wallTriggers = new StringBuilder();
        StringBuilder goalTiles = new StringBuilder();

        int rows = board.GetRows();
        int columns = board.GetCols();

        for (int row = 0; row < rows; ++row)
        {
            int rowNumber = rows - row;

            for (int col = 0; col < columns; ++col)
            {
                char colLetter = (char)('a' + col);
                string notation = $"{colLetter}{rowNumber}";

                // locations
                locations.Append($"{notation} ");

                // row connections
                if (col < columns - 1)
                {
                    rowConnections.Append($"(con {colLetter}{rowNumber} {(char)(colLetter + 1)}{rowNumber}) ");
                }

                //column connections
                if (row < rows - 1)
                {
                    columnConnections.Append($"(con {colLetter}{rowNumber} {colLetter}{rowNumber - 1}) ");
                }

                ITile tile = board.GetTileAtMatrixCoord(new Vector2Int(row, col));

                // blocked tiles
                if (tile.IsBlocked)
                {
                    blockedTiles.Append($"(blocked {notation}) ");
                }

                // wall triggers
                if (tile is WallTriggerTile wallTriggerTile)
                {
                    string trigger = PositionToNotation(wallTriggerTile.Position);
                    string wall = WallToNotation(wallTriggerTile.Wall);
                    wallTriggers.Append($"(reverse-wall-trigger {trigger} {wall}) ");
                }

                // goal tiles
                if (tile is BaseTile baseTile)
                {
                    if (baseTile.IsGoal)
                    {
                        string tileNotation = PositionToNotation(baseTile.Position);
                        foreach (Enemy enemy in board.enemies)
                        {
                            goalTiles.Append($"({enemyLocationString} {enemyNameString}{enemy.Id} {tileNotation}) ");
                        }
                        goalTiles.AppendLine();
                    }
                }
            }

            locations.AppendLine();
            locations.Append("        ");
            rowConnections.AppendLine();
            rowConnections.Append("        ");
            columnConnections.AppendLine();
            columnConnections.Append("        ");
        }

        // enemies
        StringBuilder enemies = new StringBuilder();
        foreach (Enemy enemy in board.enemies)
        {
            string enemyNotation = enemyNameString + enemy.Id;
            enemies.Append(enemyNotation + " ");
        }

        // enemies start location
        StringBuilder enemiesStartLocation = new StringBuilder();
        foreach (Enemy enemy in board.enemies)
        {
            string startLocation = PositionToNotation(enemy.Position);
            enemiesStartLocation.Append($"({enemyLocationString} {enemyNameString}{enemy.Id} {startLocation}) ");
            enemiesStartLocation.AppendLine();
            enemiesStartLocation.Append("        ");
        }

        // walls
        StringBuilder walls = new StringBuilder();
        foreach (Wall wall in board.walls)
        {
            string notation = WallToNotation(wall);
            walls.Append($"(wall {notation}) ");
        }

        string goal =
            $"(or\n" +
            $"          {goalTiles})";

        return pddlProblemTemplate
                    .Replace("{problemName}", problemName)
                    .Replace("{domainName}", domainName)
                    .Replace("{locations}", locations.ToString())
                    .Replace("{enemies}", enemies.ToString())
                    .Replace("{enemiesStartLocation}", enemiesStartLocation.ToString())
                    .Replace("{columnConnections}", columnConnections.ToString())
                    .Replace("{rowConnections}", rowConnections.ToString())
                    .Replace("{blockedLocations}", blockedTiles.ToString())
                    .Replace("{walls}", walls.ToString())
                    .Replace("{wallTriggers}", wallTriggers.ToString())
                    .Replace("{goal}", goal);
    }
}
