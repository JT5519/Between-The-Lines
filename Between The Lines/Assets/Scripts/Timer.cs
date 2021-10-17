using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public float timer;
    private bool gameOver;

    //public float timerLimitSeconds;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        timerText = GetComponent<TextMeshProUGUI>();
        //timer = timerLimitSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = GameObject.Find("GameManager").GetComponent<GameManager>().gameOver;

        if (timer > 0 && !gameOver)
        {
            timer -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);

            timerText.text = minutes + ":" + seconds;
        }
        else if (gameOver)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timerText.text = "0:00";
        }

    }
}
