using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IEnemyAction
{
    public Board Board { get; }
    public Enemy Enemy { get; }
    public Tilemap Tilemap { get; }
    public void Execute();
}
