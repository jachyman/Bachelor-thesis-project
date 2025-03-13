using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Board Board {  get; private set; }
    public int currentTurn;
    public int wallsBuildCurrentTurn;
    public bool isPlayerTurn;

    public GameState(Board board)
    {
        Board = board;
        currentTurn = 0;
        wallsBuildCurrentTurn = 0;
        isPlayerTurn = true;
    }
}
