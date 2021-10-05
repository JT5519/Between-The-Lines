using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    TMP_Text textMesh;
    Canvas canvas;

    Mesh mesh;

    Vector3[] vertices;
    [SerializeField] float wobbleAmplitude; //How intense should the wobble be

    //Variables about mouse cursor
    bool mouseGrabbingWord = false;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        float mousePosX = Input.mousePosition.x - (canvas.pixelRect.width / 2);
        float mousePosY = Input.mousePosition.y - (canvas.pixelRect.height / 2);

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;
          
            Vector3 offset = Wobble(Time.time + i);
            //Bottom Left
            vertices[index] += offset;
            //Top Left
            vertices[index + 1] += offset;
            //Top Right
            vertices[index + 2] += offset;
            //Bottom Right
            vertices[index + 3] += offset;

            //AABB collision detection
            //using the bottom left and top right vertices

            //Detecting the x axis
            if (mousePosX > vertices[index].x && mousePosX < vertices[index + 2].x)
            {
                //Detecting the y axis
                if (mousePosY > vertices[index].y && mousePosY < vertices[index + 2].y)
                {
                    Debug.Log("Over letter");
                }
            }

        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time) {
        return new Vector2(wobbleAmplitude * Mathf.Sin(time*3.3f), wobbleAmplitude * Mathf.Cos(time*2.5f));
    }
}