using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using static EnemyAIManager;
using static PDDLPlanner;

[InitializeOnLoad]
public class MeassuringScalability : MonoBehaviour
{
    [SerializeField] Board board;

    [SerializeField] [Range(1, 50)] int iterationCount;
    [SerializeField] [Range(3, 20)] int minSize;
    [SerializeField] [Range(3, 20)] int maxSize;
    [SerializeField] [Range(1, 10)] int enemyCount;
    [SerializeField] [Range(1, 10)] int goalCount;
    [SerializeField] [Range(0, 10)] int blockedTilesCount;
    [SerializeField] DomainType domainType;

    const string PDDLName = "messure";
    const string resultFolderPath = "Assets/MessuringResults";
    HashSet<(int, int)> usedPositions;
    
    class BoardGenerationParameters
    {
        public int size;
        public int enemyCount;
        public int goalCount;
        public int blockedTilesCount;

        public BoardGenerationParameters(int size, int enemyCount, int goalCount, int blockedTilesCount)
        {
            this.size = size;
            this.enemyCount = enemyCount;
            this.goalCount = goalCount;
            this.blockedTilesCount = blockedTilesCount;
        }
    }

    void Start()
    {
        Debug.Log("MEASSURING SCALABILITY");

        for ( int size = minSize; size <= maxSize; size++ )
        {
            BoardGenerationParameters parameters = new BoardGenerationParameters(
                size,
                enemyCount,
                goalCount,
                blockedTilesCount
            );
            Messure(parameters);
        }
    }

    private void Messure( BoardGenerationParameters parameters)
    {
        int successfulDownwardCount = 0;
        long time = 0;

        for (int i = 0; i < iterationCount; i++)
        {
            usedPositions = new HashSet<(int, int)>();
            GenerateBoard(parameters);

            string problemFileName = PDDLName + "_problem";
            string domainFileName = GetDomainName(domainType) + "_domain";

            CreatePDDLProblemFile(problemFileName, board, domainFileName, domainType);

            string planFileName = PDDLName + "_plan";

            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool fastDownwardReturn = FastDownwardIntegration.RunFastDownward(problemFileName, domainFileName, planFileName);
            watch.Stop();
            time += watch.ElapsedMilliseconds;

            if (fastDownwardReturn)
            {
                successfulDownwardCount++;
            }
            else
            {
                Debug.Log("no path");
            }
        }

        SaveResultToCSV(parameters, time, successfulDownwardCount);
    }

    private void GenerateBoard(BoardGenerationParameters parameters)
    {
        ITile[,] tiles = GenerateTiles(parameters.size, parameters.goalCount, parameters.blockedTilesCount);
        List<Enemy> enemies = GenerateEnemies(parameters.size, parameters.enemyCount);
        List<Wall> walls = new List<Wall>();

        board.tiles = tiles;
        board.enemies = enemies;
        board.walls = walls;
    }

    private List<Enemy> GenerateEnemies(int size, int enemyCount)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < enemyCount; ++i)
        {
            (int, int) coord = GetRandomPosition(size);
            Vector2Int position = new Vector2Int(coord.Item1, coord.Item2);

            Enemy enemy = new Enemy(position, i + 1, null, board.GetTileAt(position));
            enemies.Add(enemy);
        }
        return enemies;
    }
    private ITile[,] GenerateTiles(int size, int goalTilesCount, int blockedTilesCount)
    {
        ITile[,] tiles = new ITile[size, size];
        HashSet<(int, int)> goalTilePositions = GenerateRandomPositions(goalTilesCount, size);
        HashSet<(int, int)> blockedTilePositions = GenerateRandomPositions(blockedTilesCount, size);

        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                Vector2Int position = new Vector2Int(i, j);
                bool isGoal = goalTilePositions.Contains((i, j));
                bool isBlocked = blockedTilePositions.Contains((i, j));

                tiles[i, j] = new BaseTile(position, isBlocked, isGoal);
            }
        }

        return tiles;
    }

    private HashSet<(int, int)> GenerateRandomPositions(int count, int size)
    {
        HashSet<(int, int)> positions = new HashSet<(int, int)> ();
        for (int i = 0; i < count; ++i)
        {
            positions.Add(GetRandomPosition(size));
        }
        return positions;
    }

    private (int, int) GetRandomPosition(int size)
    {
        System.Random random = new System.Random();

        while (true)
        {
            int x = random.Next(size);
            int y = random.Next(size);

            if (usedPositions.Contains((x, y)))
            {
                continue;
            }

            usedPositions.Add((x, y));
            return (x, y);
        }
    }

    private string GetDomainName(DomainType domainType)
    {
        string domainName;
        switch (domainType)
        {
            case DomainType.NoWallTriggers:
                domainName = "no_wall_triggers";
                break;
            case DomainType.NonSimMovement:
                domainName = "wall_triggers";
                break;
            case DomainType.SimMovement:
                domainName = "sim_movement";
                break;
            default:
                Debug.LogError("no match with domain type");
                domainName = "";
                break;
        }
        return domainName;
    }

    private void SaveResultToCSV(BoardGenerationParameters parameters, long time, int iterations)
    {

        string filename = "PathfindingPerformance.csv";
        string fullPath = Path.Combine(resultFolderPath, filename);
        long averageTime = time / iterations;
        string movement = domainType == DomainType.SimMovement ? "sim" : "nonsim";

        using (var writer = new StreamWriter(fullPath, true))
        {
            //writer.WriteLine("Timestamp,Iterations,Map Size,Enemies,Goal Tiles,Avg Pathfinding Time (ms)");

            writer.WriteLine(
                $"{DateTime.Now:yyyyMMdd_HHmmss}," +
                $"{iterations}," +
                $"{parameters.size}," +
                $"{parameters.enemyCount}," +
                $"{parameters.goalCount}," +
                $"{movement}," +
                $"{averageTime}"
            );
        }

        Debug.Log($"Performance results saved to {fullPath}");
    }
}
