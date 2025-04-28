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
    [SerializeField] private List<Image> images = new List<Image>();
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
            if (images.Count > 0 && tutorialLineIndex == 2)
            {
                foreach (Image image in images)
                {
                    image.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
