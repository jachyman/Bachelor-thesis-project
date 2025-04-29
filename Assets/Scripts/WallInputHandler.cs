using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInputHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color previousColor = Color.white;
    private Color hoverColor = Color.yellow;

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
        uIManager.UpdateUI();
    }

    private void OnMouseEnter()
    {
        previousColor = spriteRenderer.color;
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = previousColor;
    }
}
