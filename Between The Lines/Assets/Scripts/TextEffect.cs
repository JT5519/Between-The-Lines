using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    TMP_Text textMesh;
    Canvas canvas;
    RectTransform canvasRect;

    Mesh mesh;

    Vector3[] vertices;
    [SerializeField] float wobbleAmplitude; //How intense should the wobble be

    //Variables about mouse cursor
    bool mouseGrabbingWord = false;

    Vector3 currentMousePos;
    Vector3 previousMousePos;
    Vector3 deltaMousePos;

    List<Vector3> previousOffset = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        currentMousePos = Vector3.zero;
        previousMousePos = Vector3.zero;       
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        currentMousePos = Input.mousePosition;
        currentMousePos.z = 10.0f;

        RectTransform rect = textMesh.rectTransform;
  
        Vector2 anchorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, currentMousePos, null, out anchorPos);
        currentMousePos = anchorPos;
       

        //Populate the previous offset array if need be
        if (previousOffset.Count <= 0)
        {
            for(int i = 0; i < textMesh.textInfo.characterCount; i++)
            {
                previousOffset.Add(Vector3.zero);
            }
        }

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
           
            //A letter will stick to the mouse if pressed
            if (Input.GetMouseButton(0))
            {
                //Updates the previous offset based on the mouse's movement
                deltaMousePos = currentMousePos - previousMousePos;
                previousOffset[i] += deltaMousePos;

                if (AABBCollision(currentMousePos, vertices[index] + previousOffset[i], vertices[index + 2] + previousOffset[i]))
                {

                    float xDistance = (Mathf.Abs(vertices[index].x - vertices[index + 2].x) / 2);
                    float yDistance = (Mathf.Abs(vertices[index].y - vertices[index + 2].y) / 2);

                    ////Bottom Left
                    vertices[index] = currentMousePos + new Vector3(-xDistance, -yDistance, 0.0f);
                    ////Top Left
                    vertices[index + 1] = currentMousePos + new Vector3(-xDistance, yDistance, 0.0f);
                    ////Top Right
                    vertices[index + 2] = currentMousePos + new Vector3(xDistance, yDistance, 0.0f);
                    ////Bottom Right
                    vertices[index + 3] = currentMousePos + new Vector3(xDistance, -yDistance, 0.0f);

                    continue;
                }
            }

            //Moves an entire letter based on the offset

            //Bottom Left
            vertices[index] += offset;
            //Top Left
            vertices[index + 1] += offset;
            //Top Right
            vertices[index + 2] += offset;
            //Bottom Right
            vertices[index + 3] += offset;

            previousOffset[i] = offset;
           
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);

        //Sets the previous mouse position for next frame
        previousMousePos = currentMousePos;
    }

    Vector2 Wobble(float time) {
        return new Vector2(wobbleAmplitude * Mathf.Sin(time*3.3f), wobbleAmplitude * Mathf.Cos(time*2.5f));
    }

    /// <summary>
    /// Collision of the mouse cursor and a rectangle
    /// </summary>
    /// <param name="mousePos">Position of mouse cursos</param>
    /// <param name="bl">Bottom left vertex of the rectangle</param>
    /// <param name="tr">top right vertex of the rectangle</param>
    /// <returns></returns>
    bool AABBCollision(Vector3 mousePos, Vector3 bl, Vector3 tr)
    {
        bool colliding = false;

        if(mousePos.x > bl.x && mousePos.x < tr.x)
        {
            if (mousePos.y > bl.y && mousePos.y < tr.y)
                colliding = true;
        }

        return colliding;
    }
}