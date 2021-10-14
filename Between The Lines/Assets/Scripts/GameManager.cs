using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string time;
    private int goal;
    private int numOfWords;

    private List<GameObject> words;

    // Start is called before the first frame update
    void Start()
    {
        goal = 3;
        numOfWords = 0;
        words = new List<GameObject>();
        time = GetComponent<TextMeshProGUI>().text;
    }

    // Update is called once per frame
    void Update()
    {
        if(time == "0:00")
        {

        }

        time = GetComponent<TextMeshProGUI>().text;
    }
}
