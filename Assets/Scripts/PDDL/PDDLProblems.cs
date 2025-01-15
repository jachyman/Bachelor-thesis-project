using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PDDLHelper;

public class PDDLProblems : MonoBehaviour
{
    Board basic_board;
    Board complex_board;

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

    void Start()
    {
        InitBoards();

        string complexProblemName = "complex_problem";
        string domainName = "my_domain";
        CreatePDDLProblemFile(complexProblemName, complex_board, domainName);

        string planName = "complex_problem_plan";
        FastDownwardIntegration.RunFastDownward(complexProblemName, domainName, planName);
    }
}
