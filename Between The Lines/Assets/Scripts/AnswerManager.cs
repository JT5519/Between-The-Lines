using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager : MonoBehaviour
{
    public char[] answers1;
    public char[] answers2;
    public char[] answers3;
    public char[] correctAnswer;
    public char[] answerSlots;
    public GameObject letterGroup1;
    public GameObject letterGroup2;
    public GameObject letterGroup3;
    public TMP_Text question1;
    public TMP_Text question2;
    public TMP_Text question3;
    public GameObject[] question1Colliders;
    public GameObject[] question2Colliders;
    public GameObject[] question3Colliders;
    public QuestionScrambler QM;
    public bool beenSolved = false; //For current testing
    public GameObject correctFeedback;
    public GameObject incorrectFeedback;
    [SerializeField] private float timeForFeedback = 3;
    public bool changedSinceCheck;

    public bool round1;
    public GameManager GM;
    private TMP_Text answerCounter;

    Coroutine routineController;
    public AnswerLine[] answerLines;
    // Start is called before the first frame update
    void Start()
    {
        answerSlots = new char[answers1.Length];
        correctFeedback.SetActive(false);
        incorrectFeedback.SetActive(false);
        changedSinceCheck = true;
        correctAnswer = answers1;
        answerCounter = transform.GetComponentInChildren<TMP_Text>();
        routineController = null;
    }

    // Update is called once per frame
    void Update()
    {

        //Check to See If Answer is Correct
        bool solved = true;
        bool empty = false;

        for (int i = 0; i < answerSlots.Length; i++)
        {
            //Check If All Answers Are Right
            if (answerSlots[i] != correctAnswer[i])
            {
                solved = false;
            }

            //Check If All Are Filled
            if (answerSlots[i] == '\0')
            {
                empty = true;
            }
        }

        if (!empty && !solved && changedSinceCheck)
        {
            routineController = StartCoroutine(WrongAnswer());
            changedSinceCheck = false;
        }

        //When Solved
        if (solved && !beenSolved)
        {
            //Debug.Log("Solved!");
            //if wrong answer feedback is still showing when right answer is entered
            if(routineController!=null)
            {
                StopCoroutine(routineController);
                incorrectFeedback.SetActive(false);
                routineController = null;
            }
            StartCoroutine(CorrectAnswer());
        }
    }

    IEnumerator CorrectAnswer()
    {
        beenSolved = true;
        correctFeedback.SetActive(true);

        answerCounter.text = (GM.numOfWords + 1) + "/3";

        yield return new WaitForSeconds(2);
        correctFeedback.SetActive(false);
        GM.numOfWords++;
        //resetting answer lines as empty
        foreach(AnswerLine aL in answerLines)
        {
            aL.SetLineFilled();
        }
        if (GM.numOfWords == 1)
        {
            answerSlots = new char[answers2.Length];
            correctAnswer = answers2;
            QM.textComponent = question2;
            foreach (GameObject c in question1Colliders)
            {
                c.SetActive(false);
            }
            foreach (GameObject c in question2Colliders)
            {
                c.SetActive(true);
            }
            QM.wordColliders = question2Colliders;
            letterGroup1.SetActive(false);
            letterGroup2.SetActive(true);
            question1.gameObject.SetActive(false);
            question2.gameObject.SetActive(true);
        }
        else if (GM.numOfWords == 2)
        {
            answerSlots = new char[answers3.Length];
            correctAnswer = answers3;
            QM.textComponent = question3;
            foreach (GameObject c in question2Colliders)
            {
                c.SetActive(false);
            }
            foreach (GameObject c in question3Colliders)
            {
                c.SetActive(true);
            }
            QM.wordColliders = question3Colliders;
            letterGroup2.SetActive(false);
            letterGroup3.SetActive(true);
            question2.gameObject.SetActive(false);
            question3.gameObject.SetActive(true);

            if (round1)
            {
                transform.GetChild(4).gameObject.SetActive(true);
                transform.GetChild(5).gameObject.SetActive(true);

                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).position += new Vector3(-20, 0, 0);
                }
            }
        }
        else if (GM.numOfWords > 2)
        {
            StartCoroutine(GM.ChangeScene());
            correctFeedback.SetActive(true);
        }

        QM.ResetQuestion();
        beenSolved = false;
    }

    IEnumerator WrongAnswer()
    {
        incorrectFeedback.SetActive(true);
        yield return new WaitForSeconds(timeForFeedback);
        incorrectFeedback.SetActive(false);
    }
}
