using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerScrambler : MonoBehaviour
{
    [SerializeField] TMP_Text[] textMesh; //array of text meshes to be wobbled
    [SerializeField] float wobbleAmplitude; //Wobble intensity
    MouseInteract[] letterMouseInteract; //Array of text mesh mouse interactions

    private void Start()
    {
        //Fill the array with text meshes' mouse interactions
        letterMouseInteract = new MouseInteract[textMesh.Length];

        for(int i = 0; i < textMesh.Length; i++)
        {
            letterMouseInteract[i] = textMesh[i].GetComponent<MouseInteract>();
        }
    }
    void Update()
    {
        //loop to wobble each textmesh in the array by an offset of i in each iteration and an offset of deltaTime every frame
        for (int i = 0; i < textMesh.Length; i++)
        {
            if(!letterMouseInteract[i].GetLetterHeldByCursor() && !letterMouseInteract[i].GetLetterInPlace())
            {
                Vector3 offset = Wobble(Time.time + i);
                textMesh[i].transform.position += offset;
            }
           
        }
    }
    //wobble generating function
    Vector2 Wobble(float time) {
        return new Vector2(wobbleAmplitude * Mathf.Sin(time*3.3f), wobbleAmplitude * Mathf.Cos(time*2.5f));
    }
}