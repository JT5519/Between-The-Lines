using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnswerScrambler : MonoBehaviour
{
    [SerializeField] TMP_Text[] textMesh; //array of text meshes to be wobbled
    [SerializeField] float wobbleAmplitude; //Wobble intensity
    MouseInteract[] letterMouseInteract; //Array of text mesh mouse interactions

    public ShockwaveSpawner shockwaveScript;

    private Rect screenBounds; //current screen size rectangle
    private Vector2[] initialAnchoredPositions; //initial letter positions 
    private void Start()
    {
        letterMouseInteract = new MouseInteract[textMesh.Length]; 
        initialAnchoredPositions = new Vector2[textMesh.Length];
        //Filling up both arrays with initial values
        for(int i = 0; i < textMesh.Length; i++)
        {
            letterMouseInteract[i] = textMesh[i].GetComponent<MouseInteract>();
            initialAnchoredPositions[i] = textMesh[i].rectTransform.anchoredPosition;
        }
        //current screen size
        screenBounds = new Rect(0, 0, Screen.width, Screen.height);
    }
    void Update()
    {
        //loop to wobble each textmesh in the array by an offset of i in each iteration and an offset of deltaTime every frame
        for (int i = 0; i < textMesh.Length; i++)
        {
            if(!letterMouseInteract[i].GetLetterHeldByCursor() && !letterMouseInteract[i].GetLetterInPlace() && !inWavePath(textMesh[i]))
            {
                //if letter goes out of the screen, reset it back to its initial position
                if (!screenBounds.Contains(textMesh[i].transform.position))
                    textMesh[i].rectTransform.anchoredPosition = initialAnchoredPositions[i];
                Vector2 offset = Wobble(Time.time + i);
                textMesh[i].rectTransform.anchoredPosition += offset;
            }
           
        }
    }
    //wobble generating function
    Vector2 Wobble(float time) {
        return new Vector2(wobbleAmplitude * Mathf.Sin(time*3.3f), wobbleAmplitude * Mathf.Cos(time*2.5f));
    }
    bool inWavePath(TMP_Text meshToCheck)
    {
        if (SceneManager.GetActiveScene().name == "CoreMinigame")
            return false;
        if (shockwaveScript.objectsShockwaveCollidesWith.Contains(meshToCheck.gameObject))
            return true;
        return false;
    }
}