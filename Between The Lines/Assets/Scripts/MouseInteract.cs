using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MouseInteract : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    private void Start()
    {
        canvas = transform.parent.parent.GetComponent<Canvas>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        AM = GameObject.FindGameObjectWithTag("AnswerManager").GetComponent<AnswerManager>();
    }


  
    //event handler when item is grabbed by the mouse
    public void OnBeginDrag(PointerEventData eventData)
    {
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
    }
    //event handler when grabbed item is dragged by the mouse
    public void OnDrag(PointerEventData eventData)
    {
        //if current letter is the one grabbed, then drag it along with the mouse
        if (iAmTheLetterHeldByCursor && cursorHoldsALetter && eventData.button == PointerEventData.InputButton.Left)
        {
            //scaling of canvas matters for movement delta
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    //event handler when grabbed item is released by the mouse
    public void OnEndDrag(PointerEventData eventData)
    {
        //end the drag if the current letter was the one being dragged
        if(iAmTheLetterHeldByCursor && cursorHoldsALetter && eventData.button == PointerEventData.InputButton.Left)
        {
            cursorHoldsALetter = false;
            iAmTheLetterHeldByCursor = false;
            canvasGroup.blocksRaycasts = true;
        }
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
