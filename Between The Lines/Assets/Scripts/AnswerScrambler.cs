using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerScrambler : MonoBehaviour
{
    [SerializeField] TMP_Text[] textMesh; //array of text meshes to be wobbled
    [SerializeField] float wobbleAmplitude; //Wobble intensity
    MouseInteract[] letterMouseInteract; //Array of text mesh mouse interactions

    public ShockwaveSpawner shockwaveScript;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        //Fill the array with text meshes' mouse interactions
        letterMouseInteract = new MouseInteract[textMesh.Length];

        for(int i = 0; i < textMesh.Length; i++)
        {
            letterMouseInteract[i] = textMesh[i].GetComponent<MouseInteract>();
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        //objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        //shockwaveScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shockwave>();
    }
    void Update()
    {
        //loop to wobble each textmesh in the array by an offset of i in each iteration and an offset of deltaTime every frame
        for (int i = 0; i < textMesh.Length; i++)
        {
            if(!letterMouseInteract[i].GetLetterHeldByCursor() && !letterMouseInteract[i].GetLetterInPlace() && !inWavePath(textMesh[i]))
            {
                Vector2 offset = Wobble(Time.time + i);
                textMesh[i].rectTransform.anchoredPosition += offset;
                //Vector2 currPos = textMesh[i].transform.position;
                //currPos.x = Mathf.Clamp(currPos.x, screenBounds.x, screenBounds.x*-1);
                //currPos.y = Mathf.Clamp(currPos.y, screenBounds.y, screenBounds.y*-1);
                //textMesh[i].transform.position = currPos;
            }
           
        }
    }
    //wobble generating function
    Vector2 Wobble(float time) {
        return new Vector2(wobbleAmplitude * Mathf.Sin(time*3.3f), wobbleAmplitude * Mathf.Cos(time*2.5f));
    }
    bool inWavePath(TMP_Text meshToCheck)
    {
        if (shockwaveScript.objectsShockwaveCollidesWith.Contains(meshToCheck.gameObject))
            return true;
        return false;
    }
}