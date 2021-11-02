using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public GameObject shockwavePrefab;
    [HideInInspector] public GameObject wave;
    
    public float shockwaveMaxSize;
    public float shockwaveSpeed;
    public int shockwaveCooldown;
    public float shockwaveHangTime; //how long it persists after growing to max size 

    private Vector3 mousePos;
    public float timerWhenWaveAbsent; //timer used when shockwave is absent
    public float timerWhenWavePresent; //timer when shockwave is present

    [HideInInspector] public HashSet<GameObject> objectsShockwaveCollidesWith;

    private Canvas mainCanvas;

    void Start()
    {
        timerWhenWaveAbsent = 0;
        timerWhenWavePresent = 0;
        objectsShockwaveCollidesWith = new HashSet<GameObject>();
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //Section of code that only runs when wave does not exist

        //Keep up with time if a wave is not present
        if (!wave)
        {
            timerWhenWaveAbsent += Time.deltaTime;
        }

        //On Mouse Click Create New Shockwave
        if (Input.GetMouseButtonDown(1) && !wave && timerWhenWaveAbsent >= shockwaveCooldown)
        {
            //Get Mouse Pos
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); 
            //Spawn the Shockwave
            wave = Instantiate(shockwavePrefab, mousePos, Quaternion.identity,mainCanvas.transform);


            timerWhenWaveAbsent = 0;
        }

        //Section of code that runs only when wave exists

        //Update the Wave if it Exists
        if (wave)
        {
            if (wave.transform.localScale.x < shockwaveMaxSize)            
            {
                wave.transform.localScale += new Vector3(Time.deltaTime * shockwaveSpeed, Time.deltaTime * shockwaveSpeed, 0);
                wave.GetComponent<CircleCollider2D>().radius = wave.transform.localScale.x / 2;
            }
            else if(timerWhenWavePresent < shockwaveHangTime)
            {
                //persist unter timer crosses hangtime
                timerWhenWavePresent += Time.deltaTime;
            }
            else if(timerWhenWavePresent >= shockwaveHangTime)
            {
                timerWhenWavePresent = 0;
                objectsShockwaveCollidesWith.Clear();
                Destroy(wave);
            }
        }
    }
}
