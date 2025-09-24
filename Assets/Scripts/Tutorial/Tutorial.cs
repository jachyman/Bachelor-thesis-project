using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    /*
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private List<Image> images = new List<Image>();
    [TextArea(3, 10)][SerializeField] private List<string> tutorialLines;
    */

    [SerializeField] private List<GameObject> gameLostGameObjectsSetTrue = new List<GameObject>();
    [SerializeField] private List<GameObject> gameLostGameObjectsSetFalse = new List<GameObject>();
    
    [SerializeField] private List<GameObject> gameWonGameObjectsSetTrue = new List<GameObject>();

    [SerializeField] private List<GameObject> wallBuiltGameObjectsSetTrue = new List<GameObject>();
    [SerializeField] private List<GameObject> wallBuiltGameObjectsSetFalse = new List<GameObject>();
    private bool wallBuilt = false;
    
    [SerializeField] private List<GameObject> undoWallGameObjectsSetTrue = new List<GameObject>();
    [SerializeField] private List<GameObject> undoWallGameObjectsSetFalse = new List<GameObject>();
    
    [SerializeField] private List<GameObject> blockedGoalGameObjectsSetTrue = new List<GameObject>();
    [SerializeField] private List<GameObject> blockedGoalObjectsSetFalse = new List<GameObject>();

    [SerializeField] private List<GameObject> hintGameObjectsSetTrue = new List<GameObject>();
    [SerializeField] private List<GameObject> hintObjectsSetFalse = new List<GameObject>();
    private bool clickedHint = false;

    [SerializeField] private List<GameObject> nextTextList = new List<GameObject>();
    private int nextTextIdx = 0;
    [SerializeField] private GameObject nextTextButton;

    [SerializeField] private GameObject nextLevelButton;
    public void GameLost()
    {
        foreach (GameObject gameObject in gameLostGameObjectsSetTrue)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in gameLostGameObjectsSetFalse)
        {
            gameObject.SetActive(false);
        }
    }

    public void GameWon()
    {
        foreach (GameObject gameObject in gameWonGameObjectsSetTrue)
        {
            gameObject.SetActive(true);
        }
    }

    public void WallBuilt()
    {
        if (!wallBuilt)
        {
            foreach (GameObject gameObject in wallBuiltGameObjectsSetTrue)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in wallBuiltGameObjectsSetFalse)
            {
                gameObject.SetActive(false);
            }
            wallBuilt = true;
        }
    }

    public void BlockeGoal()
    {
        foreach (GameObject gameObject in blockedGoalGameObjectsSetTrue)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in blockedGoalObjectsSetFalse)
        {
            gameObject.SetActive(false);
        }
    }

    public void UndoWall()
    {
        foreach (GameObject gameObject in undoWallGameObjectsSetTrue)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in undoWallGameObjectsSetFalse)
        {
            gameObject.SetActive(false);
        }
    }

    public void NextText()
    {
        if (nextTextIdx < nextTextList.Count - 1)
        {
            nextTextList[nextTextIdx].SetActive(false);
            nextTextList[nextTextIdx + 1].SetActive(true);
            if (nextTextIdx == nextTextList.Count - 2)
            {
                nextTextButton.SetActive(false);
            }
            nextTextIdx++;
        }
    }

    public void GetHint()
    {
        if (!clickedHint)
        {
            foreach (GameObject gameObject in hintGameObjectsSetTrue)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in hintObjectsSetFalse)
            {
                gameObject.SetActive(false);
            }
            clickedHint = true;
        }
    }

    public void SetActiveNextLevelButton()
    {
        nextLevelButton.SetActive(true);
    }
}
