using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public float maxTime = 180;
    public float timer;
    private bool gameOver;
    public float multiplier;
    private float maxMultiplier = 2;
    private float multiplierTimer = 0;

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
            timer -= Time.deltaTime + (Time.deltaTime * multiplier);
            multiplierTimer += Time.deltaTime;

            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);

            if (seconds < 10)
            {
                timerText.text = minutes + ":0" + seconds;
            }
            else
            {
                timerText.text = minutes + ":" + seconds;
            }

            //Every 5 seconds, increase the speed
            if (multiplierTimer >= 5 && multiplier < maxMultiplier)
            {
                multiplier += .2f;
                multiplierTimer = 0;
            }

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
