using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall
{
    private Vector2Int position;
    private bool isActive = true;
    private bool isHorizontal;
    private TileBase tileBase;

    public Vector2Int Position => position;
    public bool IsActive => isActive;
    public bool IsHorizontal => isHorizontal;
    public TileBase TileBase {  get { return tileBase; } }

    public Wall(Vector2Int positon, bool isHorizontal, TileBase tileBase)
    {
        this.position = positon;
        this.isHorizontal = isHorizontal;
        this.tileBase = tileBase;
    }

    public void SetActive(bool active)
    {
        this.isActive = active;
    }
}
