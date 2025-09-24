using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    private GameManager gameManager;
    private MySceneManager sceneManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Keyboard input: gameManager not found");
        }
        sceneManager = FindAnyObjectByType<MySceneManager>();
        if (sceneManager == null)
        {
            Debug.LogError("Keyboard input: sceneManager not found");
        }

    }

    void Update()
    {
        if (gameManager != null && sceneManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameManager.DidPlayerWon())
                {
                    sceneManager.GoToNextLevel();
                }
                else
                {
                    gameManager.StartEnemyTurn();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                sceneManager.RestartLevel();
            }
        }
    }
}
