using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public char[] answers1;
    public char[] answerSlots;
    public bool beenSolved = false; //For current testing
    public GameObject correctFeedback;
    public GameObject incorrectFeedback;
    [SerializeField] private float timeForFeedback = 3;
    public bool changedSinceCheck;

    // Start is called before the first frame update
    void Start()
    {
        answerSlots = new char[4];
        correctFeedback.SetActive(false);
        incorrectFeedback.SetActive(false);
        changedSinceCheck = true;
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
            if (answerSlots[i] != answers1[i])
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
            StartCoroutine(WrongAnswer());
            changedSinceCheck = false;
        }

        //When Solved
        if (solved && !beenSolved)
        {
            beenSolved = true;
            correctFeedback.SetActive(true);
            //Debug.Log("Solved!");
        }
    }

    IEnumerator WrongAnswer()
    {
        incorrectFeedback.SetActive(true);
        yield return new WaitForSeconds(timeForFeedback);
        incorrectFeedback.SetActive(false);
    }
}
