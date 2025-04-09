using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image tutorialImage;
    [TextArea(3, 10)][SerializeField] private List<string> tutorialLines;

    private int tutorialLineIndex;

    void Start()
    {
        tutorialLineIndex = 0;
        NextLine();
    }

    public void NextLine()
    {
        if (tutorialLineIndex < tutorialLines.Count)
        {
            tutorialText.text = tutorialLines[tutorialLineIndex];
            tutorialLineIndex++;

            if (tutorialLineIndex == tutorialLines.Count && tutorialImage != null)
            {
                tutorialImage.gameObject.SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
