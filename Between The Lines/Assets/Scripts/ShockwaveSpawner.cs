using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public GameObject shockwavePrefab;
    public float shockwaveMaxSize;
    public float shockwaveSpeed;
    public int shockwaveCooldown;
    public float shockwaveHangTime;

    private Vector3 mousePos;
    private GameObject wave;
    //private RectTransform waveTransform;
    public float timerWhenWaveAbsent;
    public float timerWhenWavePresent;

    [HideInInspector] public HashSet<GameObject> objectsShockwaveCollidesWith;


    // Start is called before the first frame update
    void Start()
    {
        timerWhenWaveAbsent = shockwaveCooldown+1;
        timerWhenWavePresent = 0;
        objectsShockwaveCollidesWith = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep up with time if a wave is not present
        if (!wave)
        {
            timerWhenWaveAbsent += Time.deltaTime;
        }


        //On Mouse Click Create New Shockwave
        if (Input.GetMouseButtonDown(1))
        {
            if (!wave && timerWhenWaveAbsent >= shockwaveCooldown)
            {
                //Get Mouse Pos
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //mousePos.z = 0f;

                //Spawn the Shockwave
                wave = Instantiate(shockwavePrefab, mousePos, Quaternion.identity);
                wave.transform.SetParent(this.transform);
                //waveTransform = wave.GetComponent<RectTransform>();

                timerWhenWaveAbsent = 0;
            }

        }


        //Update the Wave if it Exists
        if (wave)
        {
            if (wave.transform.localScale.x <= shockwaveMaxSize)
            {
                wave.transform.localScale += new Vector3(Time.deltaTime * shockwaveSpeed, Time.deltaTime * shockwaveSpeed, 0);
                wave.GetComponent<CircleCollider2D>().radius = wave.transform.localScale.x / 175;
            }
            else if (timerWhenWavePresent < shockwaveHangTime)
            {
                //persist unter timer crosses hangtime
                timerWhenWavePresent += Time.deltaTime;
            }
            else
            {
                Destroy(wave);
                objectsShockwaveCollidesWith.Clear();
                timerWhenWavePresent = 0;
            }
        }
    }
}