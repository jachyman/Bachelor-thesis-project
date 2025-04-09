using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapTile : ITile
{
    private Vector2Int position;
    private bool isOccupied;
    private UIManager uiManager;
    private int id;

    public Vector2Int Position => position;
    public bool IsBlocked
    {
        get { return isOccupied; }
    }

    public int Id { get { return id; } }
    public TrapTile(Vector2Int position, UIManager uiManager, int id)
    {
        this.position = position;
        isOccupied = false;
        this.uiManager = uiManager;
        this.id = id;
    }

    public void EnterTileEffect(Enemy enemy)
    {
        isOccupied = true;
        if (id == 0 || id == enemy.Id)
        {
            enemy.Kill();
            uiManager.KillEnemy(enemy);
        }
        else
        {
            Debug.Log("trap and enemy id not equal");
        }
    }

    public void LeaveTileEffect()
    {
        isOccupied = false;
    }
}
