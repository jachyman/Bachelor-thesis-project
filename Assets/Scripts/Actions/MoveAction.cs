using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveAction : IEnemyAction
{
    private ITile targetTile;
    private Enemy enemy;
    private Board board;
    private Tilemap tilemap;

    public MoveAction(Enemy enemy, ITile targetTile, Board board, Tilemap tilemap)
    {
        this.targetTile = targetTile;
        this.enemy = enemy;
        this.board = board;
        this.tilemap = tilemap;
    }

    public Enemy Enemy => enemy;
    public Board Board { get { return board; } }
    public Tilemap Tilemap { get { return tilemap; } }

    public void Execute()
    {
        if (!enemy.IsAlive)
        {
            Debug.Log("Execute action: enemy is not alive");
            return;
        }
        enemy.MoveTo(targetTile, tilemap);
    }
}
