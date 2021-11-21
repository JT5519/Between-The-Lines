using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using TMPro;

public class QuestionScrambler : MonoBehaviour
{
    public TMP_Text textComponent;
    [SerializeField] float scramblePeriod; //periodicity of each word being scrambled
    [SerializeField] float scrambleOffset; //relative offset of scrambling between consecutive words
    float timer; //timer to measure time in current period

    
    private List<string> words; //list to hold words list of the question
    private string[] initialQuestionWords; //saves the initial questions words
    bool[] isScrambled; //array to hold scrambled status of each word (assumed that number of words in question remains constant)

    public GameObject[] wordColliders; //array holding collider objects for each question word
    public ShockwaveSpawner shockwaveScript;
    private void Start()
    {
        timer = 0;
        //initialising word list
        words = new List<string>(Regex.Split(textComponent.text, @"[\s?]"));
        words.RemoveAll(word => word.Length == 0);
        //initialising and populating initialQuestionWords
        initialQuestionWords = new string[words.Count];
        for (int i = 0; i < initialQuestionWords.Length; i++)
            initialQuestionWords[i] = words[i];
        //intializing and populating isScrambled array
        isScrambled = new bool[words.Count];
        for (int i = 0; i < isScrambled.Length; i++)
            isScrambled[i] = false;
    }

    void Update()
    {
        //timer update
        timer += Time.deltaTime;
        //check for period reset 
        ResetTimerAndScrambler();
        //list containing words in the question
        words = new List<string>(Regex.Split(textComponent.text, @"[\s?]"));
        words.RemoveAll(word => word.Length == 0);
        //loop to iterate through the list
        foreach (int index in Enumerable.Range(0, words.Count))
        {
            //for each word, scramble it if not scrambled yet, based on its relative offset and scramble period
            if(inWavePath(index))
            {
                words[index] = initialQuestionWords[index];
                textComponent.text = string.Join(" ", words) + "?";
                continue;
            }
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
    //reset timer and isScrambled array when period ends
    void ResetTimerAndScrambler()
    {
        if (timer > scramblePeriod)
        {
            timer = 0;
            for (int i = 0; i < isScrambled.Length; i++)
                isScrambled[i] = false;
        }
    }

    public void ResetQuestion()
    {
        timer = 0;
        words = new List<string>(Regex.Split(textComponent.text, @"[\s?]"));
        words.RemoveAll(word => word.Length == 0);
        //initialising and populating initialQuestionWords
        initialQuestionWords = new string[words.Count];
        for (int i = 0; i < initialQuestionWords.Length; i++)
            initialQuestionWords[i] = words[i];
        //intializing and populating isScrambled array
        isScrambled = new bool[words.Count];
        for (int i = 0; i < isScrambled.Length; i++)
            isScrambled[i] = false;
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
    bool inWavePath(int wordIndex)
    {
        if (shockwaveScript.objectsShockwaveCollidesWith.Contains(wordColliders[wordIndex]))
            return true;
        return false;
    }

}
