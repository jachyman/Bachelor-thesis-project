using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour, IEnemyAction
{
    private ITile targetTile;
    private Enemy enemy;
    private Board board;

    public MoveAction(Enemy enemy, ITile targetTile, Board board)
    {
        this.targetTile = targetTile;
        this.enemy = enemy;
        this.board = board;
    }

    public Enemy Enemy => enemy;
    public Board Board { get { return board; } }

    public void Execute()
    {
        enemy.MoveTo(targetTile);
    }
}
