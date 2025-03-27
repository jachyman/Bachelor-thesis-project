using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapTile : ITile
{
    private Vector2Int position;
    private bool isBlocked;
    private UIManager uiManager;

    public Vector2Int Position => position;
    public bool IsBlocked
    {
        get { return isBlocked; }
        set { isBlocked = value; }
    }
    public TrapTile(Vector2Int position, UIManager uiManager)
    {
        this.position = position;
        isBlocked = false;
        this.uiManager = uiManager;
    }

    public void TriggerEffect(Enemy enemy)
    {
        enemy.Kill();
        uiManager.KillEnemy(enemy);
        IsBlocked = true;
    }
}
