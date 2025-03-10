using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Board Board {  get; private set; }
    public int CurrentTurn { get; set; }

    public GameState(Board board)
    {
        Board = board;
        CurrentTurn = 0;
    }
}
