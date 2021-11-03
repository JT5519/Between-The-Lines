using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public GameObject shockwavePrefab;
    public float shockwaveMaxSize;
    public float shockwaveSpeed;
    public int shockwaveCooldown;
    private Vector3 mousePos;
    private GameObject wave;
    //private RectTransform waveTransform;
    public float timer;

   [HideInInspector] public HashSet<GameObject> objectsShockwaveCollidesWith;


    // Start is called before the first frame update
    void Start()
    {
        objectsShockwaveCollidesWith = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep up with time if a wave is not present
        if (!wave)
        {
            timer += Time.deltaTime;
        }


        //On Mouse Click Create New Shockwave
        if (Input.GetMouseButtonDown(1))
        {
            if (!wave && timer >= shockwaveCooldown)
            {
                //Get Mouse Pos
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //mousePos.z = 0f;

                //Spawn the Shockwave
                wave = Instantiate(shockwavePrefab, mousePos, Quaternion.identity);
                wave.transform.SetParent(this.transform);
                //waveTransform = wave.GetComponent<RectTransform>();

                timer = 0;
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
            else
            {
                Destroy(wave);
                objectsShockwaveCollidesWith.Clear();
            }
        }
    }
}