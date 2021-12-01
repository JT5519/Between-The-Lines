using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject timer;
    private float time;
    private int goal;
    public int numOfWords;
    private GameObject answerManager;
    public bool gameOver;
    [SerializeField] private int nextSceneID;
    [SerializeField] private float timeBetweenScenes;


    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer");
        answerManager = GameObject.FindGameObjectWithTag("AnswerManager");
        time = timer.GetComponent<Timer>().timer;
        goal = 3;
        numOfWords = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool beenSolved = answerManager.GetComponent<AnswerManager>().beenSolved;

        //Checks if the player has gotten a word on the page
        if(beenSolved == true)
        {
            //numOfWords = numOfWords + 1;
        }

        //Triggers game over event if the player fails to get three words before time runs out
        if(time == 0 && numOfWords != goal)
        {
            int wordsLeft = goal - numOfWords;
            //Debug.Log("Sorry, You Failed..." + "\n" + "You had " + wordsLeft + " words to go.");
            gameOver = true;
        }

        //Checks if the player has reached the goal -- getting three words on the page
        if(numOfWords == goal)
        {
            //Debug.Log("Way to Go!" + "\n" + "You got three in a row!");
            gameOver = true;
            StartCoroutine(ChangeScene());
        }
    }

    public IEnumerator ChangeScene()
    {
        gameOver = true;
        yield return new WaitForSeconds(timeBetweenScenes);
        SceneManager.LoadScene(nextSceneID);
    }
}
