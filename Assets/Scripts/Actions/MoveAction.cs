using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MoveAction : IEnemyAction
{
    private ITile targetTile;
    private Enemy enemy;
    private UIManager uiManager;

    public MoveAction(Enemy enemy, ITile targetTile, UIManager uiManager)
    {
        this.targetTile = targetTile;
        this.enemy = enemy;
        this.uiManager = uiManager;
    }

    public void Execute()
    {
        if (!enemy.IsAlive)
        {
            Debug.Log("Execute action: enemy is not alive");
            return;
        }
        if (!targetTile.IsBlocked)
        {
            uiManager.MoveEnemyToTile(enemy, targetTile);
            enemy.MoveTo(targetTile);
        }
        else
        {
            Debug.Log("Enemy MoveTo: tile is blocked");
        }
        
    }
}
