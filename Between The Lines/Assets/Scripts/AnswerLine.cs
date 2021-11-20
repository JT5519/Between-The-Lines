using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerLine : MonoBehaviour, IDropHandler
{
    public int lineIndex;
    private bool lineFilled = false;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !lineFilled)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;

            //Sets a trigger to prevent wobble
            eventData.pointerDrag.GetComponent<MouseInteract>().SetLetterInPlace();

            char[] answerSlots = GetComponentInParent<AnswerManager>().answerSlots;

            answerSlots[lineIndex] = char.Parse(eventData.pointerDrag.GetComponent<TextMeshProUGUI>().text.ToLower());
            eventData.pointerDrag.GetComponent<MouseInteract>().answerLinePlacedIn = lineIndex;
            //line has a letter occupying it
            lineFilled = true;
        }
    }
    public bool GetLineFilled()
    {
        return lineFilled;
    }

    public void SetLineFilled() => lineFilled = false;
}
