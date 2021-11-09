using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text[] tutorialText;
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

    private void Start()
    {
        UpdateText();
        nextButtonText = nextButton.GetComponentInChildren<Text>();
        backgroundImage = backgroundPanel.GetComponent<Image>();

        originalSprite = backgroundImage.sprite;
    }

    private void Update()
    {
        UpdateButtons();

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
        textPlaceHold.font = tutorialText[tutorialIndex].font;
        textPlaceHold.alignment = tutorialText[tutorialIndex].alignment;
        textPlaceHold.color = tutorialText[tutorialIndex].color;
        textPlaceHold.text = tutorialText[tutorialIndex].text;
    }

    void UpdateButtons()
    {
        //Disable previous button on the tutorial's first page
        if (tutorialIndex <= 0)
            previousButton.gameObject.SetActive(false);
        else
            previousButton.gameObject.SetActive(true);

        //Change the next button text on the last page of the tutorial
        if (tutorialIndex >= tutorialText.Length - 1)
            nextButtonText.text = "Play Game";
        else
            nextButtonText.text = "Next Page";
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
