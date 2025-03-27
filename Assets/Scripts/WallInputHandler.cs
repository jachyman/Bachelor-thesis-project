using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInputHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;

    private void Start()
    {
        uIManager = FindAnyObjectByType<UIManager>();
        if (uIManager == null)
        {
            Debug.LogError("WallInputHandler UIManager not found");
        }
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("WallInputHandler GameManager not found");
        }
    }

    private void OnMouseDown()
    {
        Wall wall = uIManager.GenerateWall(gameObject);
        gameManager.BuildWall(wall);
    }
}
