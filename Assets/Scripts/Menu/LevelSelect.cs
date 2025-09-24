using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public List<Button> levelButtons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        Debug.Log("level at " + levelAt);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i >= levelAt)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
