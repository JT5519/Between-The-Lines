using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using TMPro;

public class QuestionScrambler : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] float scramblePeriod; //periodicity of each word being scrambled
    [SerializeField] float scrambleOffset; //relative offset of scrambling between consecutive words
    float timer; //timer to measure time in current period
    bool[] isScrambled; //array that maintains if each word has been scrambled in the current period or not
    // Update is called once per frame
    private void Start()
    {
        timer = 0;
    }

    void Update()
    {
        //timer update
        TimerController();
        //check for period reset 
        ResetTimerAndScrambler();
        //list containing words in the question
        List<string> words = new List<string>(Regex.Split(textComponent.text, @"[\s?]"));
        words.RemoveAll(word => word.Length == 0);
        //initialize isScrambled array if not done
        if (isScrambled == null)
        {
            populateIsScrambledArrayAsFalse(words.Count);
        }
        //loop to iterate through the list
        foreach (int index in Enumerable.Range(0, words.Count))
        {
            //for each word, scramble it if not scrambled yet, based on its relative offset and scramble period
            if (timer > (scrambleOffset * index)%scramblePeriod && !isScrambled[index])
            {
                //replace original word with scrambled word
                words[index] = WordScramble(new StringBuilder(words[index]));
                //recombine words in the list with space as a separator and add a question mark to the end and set it to text
                textComponent.text = string.Join(" ", words) + "?";
                //record that word has been scrambled for current period
                isScrambled[index] = true;
            }
        }
    }
    //update timer function
    void TimerController()
    {
        timer += Time.deltaTime;
    }
    //reset timer and isScrambled array when period ends
    void ResetTimerAndScrambler()
    {
        if (timer > scramblePeriod)
        {
            timer = 0;
            if (isScrambled != null)
            {
                populateIsScrambledArrayAsFalse();
            }
        }
    }
    //populate / re-populate the isScrambled array 
    void populateIsScrambledArrayAsFalse(int size=-1)
    {
        //if size is not passed as parameter, it means array is only being reset
        if(size!=-1)
        {
            //if size is passed as parameter, a new array must be created
            isScrambled = new bool[size];
        }
        //populate array
        for(int i = 0;i<isScrambled.Length;i++)
        {
            isScrambled[i] = false;
        }
    }
    //scrambling a word
    string WordScramble(StringBuilder word)
    {
        int index = (Random.Range(0, word.Length-1));
        char temp = word[index];
        word[index] = word[index+1];
        word[index+1] = temp;
        return word.ToString();
    }
    
}
