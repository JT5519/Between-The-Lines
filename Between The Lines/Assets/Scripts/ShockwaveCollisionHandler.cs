using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollisionHandler : MonoBehaviour
{
    HashSet<GameObject> objectsICollideWith;
    private bool audioPlaying = false;
    private void Start()
    {
        //everytime the shockwave is instantiated, this runs;
        objectsICollideWith = transform.parent.GetComponent<ShockwaveSpawner>().objectsShockwaveCollidesWith;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsICollideWith.Add(other.gameObject);
        if (!audioPlaying)
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            audioPlaying = true;
        }
        Debug.Log("Triggered object: " + other.gameObject.name);
    }
}
