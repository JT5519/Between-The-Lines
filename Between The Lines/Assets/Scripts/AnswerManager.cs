using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public char[] answers1;
    public char[] answerSlots;
    public bool beenSolved = false; //For current testing

    // Start is called before the first frame update
    void Start()
    {
        answerSlots = new char[4];
    }

    // Update is called once per frame
    void Update()
    {

        //Check to See If Answer is Correct
        bool solved = true;

        for (int i = 0; i < answerSlots.Length; i++)
        {
            if (answerSlots[i] != answers1[i])
            {
                solved = false;
                return;
            }
        }

        //When Solved
        if (solved && !beenSolved)
        {
            beenSolved = true;
            Debug.Log("Solved!");
        }
    }
}
