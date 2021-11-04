using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("collided object: " + other.name);
    }
}
