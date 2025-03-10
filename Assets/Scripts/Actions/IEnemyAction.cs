using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAction
{
    public Board Board { get; }
    public Enemy Enemy { get; }
    public void Execute();
}
