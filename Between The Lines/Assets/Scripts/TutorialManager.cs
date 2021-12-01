using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialText;
    [SerializeField] private Text textPlaceHold;

    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private Text nextButtonText;

    [SerializeField] private GameObject backgroundPanel;
    private Image backgroundImage;
    [SerializeField] private Sprite blankBoardSprite;
    private Sprite originalSprite;

    [SerializeField] private int levelToTransition = 1;

    private int tutorialIndex = 0;

    void Start()
    {       
        nextButtonText = nextButton.GetComponentInChildren<Text>();
        backgroundImage = backgroundPanel.GetComponent<Image>();
        originalSprite = backgroundImage.sprite;

        UpdateText();
    }

    void Update()
    {
        //UpdateButtons();
       
        //Change background past the title page
        if (tutorialIndex > 0)
            backgroundImage.sprite = blankBoardSprite;
        else
            backgroundImage.sprite = originalSprite;
    }

    /// <summary>
    /// Change the current text when needed
    /// </summary>
    void UpdateText()  
    {
        Text currentText = tutorialText[tutorialIndex].GetComponent<Text>();
        textPlaceHold.font = currentText.font;
        textPlaceHold.alignment = currentText.alignment;
        textPlaceHold.color = currentText.color;
        textPlaceHold.text = currentText.text;

        UpdateButtons();
    }

    void UpdateButtons()
    {
        //Disable previous button on the tutorial's first page
        if (tutorialIndex <= 0)
            previousButton.gameObject.SetActive(false);
        else
            previousButton.gameObject.SetActive(true);

        //Checks to see if the question has an answer
        TutorialButtonAnswer answer = tutorialText[tutorialIndex].GetComponent<TutorialButtonAnswer>();
        string buttonText = answer ? answer.GetButtonText() : "Next Page";

        nextButtonText.text = buttonText;
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
