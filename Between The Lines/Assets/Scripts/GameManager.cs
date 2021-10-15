using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject timer;
    private float time;
    private int goal;
    private int numOfWords;


    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer");
        time = timer.GetComponent<Timer>().timer;
        goal = 3;
        numOfWords = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
