using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        uiManager.MoveEnemyToTile(enemy, targetTile);
        enemy.MoveTo(targetTile);
    }
}
