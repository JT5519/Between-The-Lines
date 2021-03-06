using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MouseInteract : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler //, IEndDragHandler//, ISelectHandler, IBeginDragHandler,
{
    //The canvas that is the parent for the group of letters. Need it for its scaleFactor when 
    // moving objects with their mouse deltas
    [SerializeField] private Canvas canvas;
    
    //rect transform of the current textmesh
    private RectTransform rect;

    //Canvas Group
    private CanvasGroup canvasGroup;

    //static variable that is used to ensure the cursor only grabs one letter at a time. 
    // TRUE = A particular letter is held, FALSE = No Letter is held
    private static bool cursorHoldsALetter;
    // private variable for each textmesh to know whether it is the letter held by the cursor
    private bool iAmTheLetterHeldByCursor;
    // private variable to test 
    private bool letterInPlace;
    public int answerLinePlacedIn = -1;
    public AnswerManager AM;

    private AudioSource sound;

    private void Start()
    {
        canvas = transform.parent.parent.GetComponent<Canvas>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        AM = GameObject.FindGameObjectWithTag("AnswerManager").GetComponent<AnswerManager>();
        sound = GetComponent<AudioSource>();
    }

    //event handler when item is grabbed by the mouse
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!cursorHoldsALetter && !iAmTheLetterHeldByCursor && eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log(gameObject.name + " grabbed");
            cursorHoldsALetter = true;
            iAmTheLetterHeldByCursor = true;
            canvasGroup.blocksRaycasts = false;
            letterInPlace = false;

            //if grabbed textmesh was on an answer line
            if (answerLinePlacedIn >= 0)
                resetAnswerLine();  
        }
    }
    //event handler when grabbed item is dragged by the mouse
    public void OnDrag(PointerEventData eventData)
    {
        //if current letter is the one grabbed, then drag it along with the mouse
        if (iAmTheLetterHeldByCursor && cursorHoldsALetter && eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log(gameObject.name + " being dragged grabbed");
            //scaling of canvas matters for movement delta
            rect.anchoredPosition += eventData.delta/canvas.scaleFactor;
        }

    }

    //event handler when grabbed item is released by the mouse
    public void OnPointerUp(PointerEventData eventData)
    {
        if (iAmTheLetterHeldByCursor && cursorHoldsALetter && eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log(gameObject.name + " released");
            cursorHoldsALetter = false;
            iAmTheLetterHeldByCursor = false;
            canvasGroup.blocksRaycasts = true;
        }
    }

    //discared event handlers
    /*public void OnEndDrag(PointerEventData eventData)
    {
        //end the drag if the current letter was the one being dragged
        if(iAmTheLetterHeldByCursor && cursorHoldsALetter && eventData.button == PointerEventData.InputButton.Left)
        {
            cursorHoldsALetter = false;
            iAmTheLetterHeldByCursor = false;
            canvasGroup.blocksRaycasts = true;
        }
    }*/
    /*private void OnMouseDrag()
{
    //if cursor is free, then allow current letter to be grabbed
    if (!cursorHoldsALetter && !iAmTheLetterHeldByCursor && Input.GetMouseButton(0))
    {
        cursorHoldsALetter = true;
        iAmTheLetterHeldByCursor = true;
        canvasGroup.blocksRaycasts = false;
        letterInPlace = false;
        if (answerLinePlacedIn >= 0)
        {
            AM.changedSinceCheck = true;
            AM.answerSlots[answerLinePlacedIn] = '\0';
        }
        answerLinePlacedIn = -1;
    }

}*/
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begun");
        //if cursor is free, then allow current letter to be grabbed
        if(!cursorHoldsALetter && !iAmTheLetterHeldByCursor && eventData.button == PointerEventData.InputButton.Left)
        {
            cursorHoldsALetter = true;
            iAmTheLetterHeldByCursor = true;
            canvasGroup.blocksRaycasts = false;
            letterInPlace = false;
            if (answerLinePlacedIn >= 0)
            {
                AM.changedSinceCheck = true;
                AM.answerSlots[answerLinePlacedIn] = '\0';
            }
            answerLinePlacedIn = -1;
        }
    }*/
    public void resetAnswerLine()
    {

        AM.changedSinceCheck = true;
        AM.answerSlots[answerLinePlacedIn] = '\0';
        AM.answerLines[answerLinePlacedIn].SetLineFilled();
        answerLinePlacedIn = -1;
    }
    public bool GetLetterHeldByCursor()
    {
        return iAmTheLetterHeldByCursor;
    }

    public bool GetLetterInPlace()
    {
        return letterInPlace;
    }

    public void SetLetterInPlace() => letterInPlace = true;





    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Collided with: " + collision.name);
    //}


}
