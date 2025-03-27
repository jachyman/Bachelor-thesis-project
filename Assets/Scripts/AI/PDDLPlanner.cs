using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static GameManager;
using UnityEngine.Tilemaps;
using static EnemyAIManager;

public class PDDLPlanner : MonoBehaviour
{
    //private const string PDDLPath = "Assets/PDDL/";
    private static string PDDLPath;
    private static string PDDLDomainPath;

    private const string moveActionString = "move";
    private const string skipGoalReachedEnemyActionString = "skip_goal_reached_enemy";
    private const string enemyNameString = "en";
    private const string enemyLocationString = "enemy_loc";
    private const string nextEnemyTurnString = "next_turn";

    private void Awake()
    {
        PDDLPath = Application.persistentDataPath + "/";
    }

    public static bool SolveProblem(Board board, string problemName, string domainName, DomainType domainType)
    {
        string problemFileName = problemName + "_problem";
        string domainFileName = domainName + "_domain";

        CreatePDDLProblemFile(problemFileName, board, domainFileName, domainType);

        string planFileName = problemName + "_plan";

        var watch = System.Diagnostics.Stopwatch.StartNew();
        bool fastDownwardReturn = FastDownwardIntegration.RunFastDownward(problemFileName, domainFileName, planFileName);
        watch.Stop();
        Debug.Log("fast downward time: " + watch.ElapsedMilliseconds + " ms");

        return fastDownwardReturn;
    }

    public static List<IEnemyAction> GetActionsFromPlan(string planName, Board board, Tilemap onGroundTilemap)
    {
        string path = PDDLPath + planName + "_plan.pddl";
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

                                MoveAction moveAction = new MoveAction(enemy, toTile, board, onGroundTilemap);
                                actions.Add(moveAction);
                                break;
                            case skipGoalReachedEnemyActionString:
                                //Debug.Log("skip enemy action");
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

    private static void CreatePDDLProblemFile(string problemFileName, Board board, string domain, DomainType domainType)
    {
        string pddlContent = GeneratePDDProblem(problemFileName, board, domain, domainType);
        //string filePath = Application.persistentDataPath + $"/{problemFileName}.pddl";
        string filePath = Path.Combine(Application.persistentDataPath, $"{problemFileName}.pddl");

        File.WriteAllText(filePath, pddlContent);
        //Debug.Log($"PDDL file written to {filePath}");
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
        
        {nextEnemyTurn}

        {currentTurn}
        
        ; column connections
        {columnConnections}
        
        ; row connections
        {rowConnections}
        
        {blockedLocations}
        
        {walls}

        {wallTriggers}

        {goalTiles}
    )
    
    (:goal
        {goal}
    )

    (:metric minimize (total-cost))
)";

    private static string GeneratePDDProblem(string name, Board board, string domain, DomainType domainType)
    {
        string problemName = name;
        string domainName = domain;

        StringBuilder locations = new StringBuilder();
        StringBuilder rowConnections = new StringBuilder();
        StringBuilder columnConnections = new StringBuilder();
        StringBuilder blockedTiles = new StringBuilder();
        StringBuilder wallTriggers = new StringBuilder();
        //StringBuilder goalTiles = new StringBuilder();
        StringBuilder goalTilesNew = new StringBuilder();
        List<string> goalTilesNotation = new List<string>();

        int rows = board.GetRows();
        int columns = board.GetCols();

        // BOARD
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
                        goalTilesNew.Append($"(goal_tile {tileNotation}) ");

                        goalTilesNotation.Add(tileNotation);
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

        // ENEMIES
        string currentTurn = "";
        if (domainType == DomainType.SimMovement)
        {
            Enemy firstAliveEnemy = board.GetFirstAliveEnemy();
            currentTurn = $"(current_turn en{firstAliveEnemy.Id})";
        }
        StringBuilder enemies = new StringBuilder();
        List<string> enemiesNotation = new List<string>();
        StringBuilder enemiesStartLocation = new StringBuilder();
        StringBuilder nextEnemyTurn = new StringBuilder();

        foreach (Enemy enemy in board.enemies)
        {
            if (enemy.IsAlive)
            {
                string enemyNotation = enemyNameString + enemy.Id;

                // enemies
                enemies.Append(enemyNotation + " ");
                enemiesNotation.Add(enemyNotation);

                // start locations
                string startLocation = PositionToNotation(enemy.Position);
                enemiesStartLocation.Append($"({enemyLocationString} {enemyNameString}{enemy.Id} {startLocation}) ");
                enemiesStartLocation.AppendLine();
                enemiesStartLocation.Append("        ");

                // blocked
                blockedTiles.Append($"(blocked {startLocation}) ");

                if (domainType == DomainType.SimMovement)
                {
                    // enemy next turn
                    Enemy nextAliveEnemy = board.GetNextAliveEnemy(enemy);
                    string nextEnemyNotation = enemyNameString + (nextAliveEnemy.Id);
                    nextEnemyTurn.Append($"({nextEnemyTurnString} {enemyNotation} {nextEnemyNotation})");
                    nextEnemyTurn.AppendLine();
                    nextEnemyTurn.Append("        ");
                }
            }
        }

        // walls
        StringBuilder walls = new StringBuilder();
        foreach (Wall wall in board.walls)
        {
            string notation = WallToNotation(wall);
            walls.Append($"(wall {notation}) ");
        }

        // goal condition
        StringBuilder goalReached = new StringBuilder();
        foreach (string enemyNotation in enemiesNotation)
        {
            goalReached.Append($"(goal_reached {enemyNotation})");
            goalReached.AppendLine();
            goalReached.Append("          ");
        }
        string operation = domainType == DomainType.SimMovement ? "and" : "or";
        string goal =
        $"({operation}\n" +
        $"          {goalReached.ToString()})";


        return pddlProblemTemplate
                    .Replace("{problemName}", problemName)
                    .Replace("{domainName}", domainName)
                    .Replace("{locations}", locations.ToString())
                    .Replace("{enemies}", enemies.ToString())
                    .Replace("{enemiesStartLocation}", enemiesStartLocation.ToString())
                    .Replace("{nextEnemyTurn}", nextEnemyTurn.ToString())
                    .Replace("{currentTurn}", currentTurn)
                    .Replace("{columnConnections}", columnConnections.ToString())
                    .Replace("{rowConnections}", rowConnections.ToString())
                    .Replace("{blockedLocations}", blockedTiles.ToString())
                    .Replace("{walls}", walls.ToString())
                    .Replace("{wallTriggers}", wallTriggers.ToString())
                    .Replace("{goalTiles}", goalTilesNew.ToString())
                    .Replace("{goal}", goal);
    }

    private static string GenerateEnemyGoalCombinations(List<string> enemies, List<string> goals)
    {
        StringBuilder combination = new StringBuilder();
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < goals.Count; j++)
            {
                combination.Append("(and ");

                List<string> newEnemies = new List<string>(enemies);
                newEnemies.RemoveAt(i);
                List<string> newGoals = new List<string>(goals);
                newGoals.RemoveAt(j);

                combination.Append($"(enemy_loc {enemies[i]} {goals[j]}) ");
                combination.Append(GenerateEnemyGoalCombinationsRec(newEnemies, newGoals));

                combination.Append(")");
                combination.AppendLine();
                combination.Append("        ");
            }
        }

        return combination.ToString();
    }

    private static string GenerateEnemyGoalCombinationsRec(List<string> enemies, List<string> goals)
    {
        if (enemies.Count == 0)
        {
            return "";
        }

        StringBuilder combination = new StringBuilder();
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < goals.Count; j++)
            {
                List<string> newEnemies = new List<string>(enemies);
                newEnemies.RemoveAt(i);
                List<string> newGoals = new List<string>(goals);
                newGoals.RemoveAt(j);

                combination.Append($"(enemy_loc {enemies[i]} {goals[j]}) ");
                string newCombination = GenerateEnemyGoalCombinationsRec(newEnemies, newGoals);
            }
        }

        return combination.ToString();
    }
}
