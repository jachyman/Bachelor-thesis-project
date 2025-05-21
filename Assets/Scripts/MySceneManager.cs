using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    const int levelIndexOffset = 2;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index > SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        else if (SceneManager.GetActiveScene().buildIndex - levelIndexOffset == PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", PlayerPrefs.GetInt("levelAt") + 1);
        }
        SceneManager.LoadScene(index);
    }

    public void GoToLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber + levelIndexOffset);
    }
}
