using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    public Vector2Int Position { get; }
    public bool IsBlocked { get; }
    public void EnterTileEffect(Enemy enemy);
    public void LeaveTileEffect();
}
