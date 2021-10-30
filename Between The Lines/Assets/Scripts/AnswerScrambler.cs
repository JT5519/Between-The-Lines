using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerScrambler : MonoBehaviour
{
    [SerializeField] TMP_Text[] textMesh; //array of text meshes to be wobbled
    [SerializeField] float wobbleAmplitude; //Wobble intensity
    MouseInteract[] letterMouseInteract; //Array of text mesh mouse interactions

    Shockwave shockwaveScript;
    private void Start()
    {
        //Fill the array with text meshes' mouse interactions
        letterMouseInteract = new MouseInteract[textMesh.Length];

        for(int i = 0; i < textMesh.Length; i++)
        {
            letterMouseInteract[i] = textMesh[i].GetComponent<MouseInteract>();
        }

        shockwaveScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shockwave>();
    }
    void Update()
    {
        //loop to wobble each textmesh in the array by an offset of i in each iteration and an offset of deltaTime every frame
        for (int i = 0; i < textMesh.Length; i++)
        {
            if(!letterMouseInteract[i].GetLetterHeldByCursor() && !letterMouseInteract[i].GetLetterInPlace() && !inWavePath(textMesh[i]))
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
    bool inWavePath(TMP_Text meshToCheck)
    {
        if (shockwaveScript.objectsShockwaveCollidesWith.Contains(meshToCheck.gameObject))
            return true;
        return false;
    }
}