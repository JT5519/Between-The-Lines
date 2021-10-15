using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerLine : MonoBehaviour, IDropHandler
{
    public int lineIndex;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;

            char[] answerSlots = GetComponentInParent<AnswerManager>().answerSlots;

            answerSlots[lineIndex] = char.Parse(eventData.pointerDrag.GetComponent<TextMeshProUGUI>().text.ToLower());
        }
    }
}
