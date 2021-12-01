using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonAnswer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string answerButtonText;

    public string GetButtonText()
    {
        return answerButtonText;
    }
}
