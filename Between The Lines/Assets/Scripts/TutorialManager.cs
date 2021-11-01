using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text[] tutorialText;
    [SerializeField] private Text textPlaceHold;

    [SerializeField] private int levelToTransition = 1;

    private int tutorialIndex = 0;

    private void Start()
    {
        UpdateText();
    }

    /// <summary>
    /// Change the current text when needed
    /// </summary>
    void UpdateText()  
    {
        textPlaceHold.font = tutorialText[tutorialIndex].font;
        textPlaceHold.alignment = tutorialText[tutorialIndex].alignment;
        textPlaceHold.color = tutorialText[tutorialIndex].color;
        textPlaceHold.text = tutorialText[tutorialIndex].text;
    }

    /// <summary>
    /// Transitions to the next tutorial page
    /// </summary>
    public void ToNextPage()
    {
        //Load main game at the end of the tutorial
        if(tutorialIndex >= tutorialText.Length - 1)
        {
            SceneManager.LoadScene(levelToTransition);
        }
        else
        {
            tutorialIndex++;
            UpdateText();
        }
        
    }

    //Transitions to the previous tutorial page
    public void ToPreviousPage()
    {
        //Can't transition before the first page
        if(tutorialIndex > 0)
        {
            tutorialIndex--;
            UpdateText();
        }
    }
}
