using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
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
        Debug.Log("levelAt " + PlayerPrefs.GetInt("levelAt"));
        if (PlayerPrefs.GetInt("levelAt") == 0)
        {
            SceneManager.LoadScene("Level_0");
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void GoToNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index > SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        else //if (SceneManager.GetActiveScene().name - levelIndexOffset == PlayerPrefs.GetInt("levelAt"))
        {
            string[] splitSceneName = SceneManager.GetActiveScene().name.Split("_");
            if (splitSceneName[0] == "Level")
            {
                int finishedLevelIndex = Int32.Parse(splitSceneName[1]);
                //Debug.Log("finished level idx " + finishedLevelIndex);
                //Debug.Log("level at " + finishedLevelIndex);
                if (finishedLevelIndex == PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", finishedLevelIndex + 1);
                }
            }
        }
        SceneManager.LoadScene(index);
    }

    public void GoToLevel(int levelNumber)
    {

        //SceneManager.LoadScene(levelNumber + levelIndexOffset);
        SceneManager.LoadScene("Level_" + levelNumber);
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("levelAt", 0);
    }
}
