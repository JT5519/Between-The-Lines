using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollisionHandler : MonoBehaviour
{
    HashSet<GameObject> objectsICollideWith;
    private void Start()
    {
        //evertime the shockwave is instantiated, this runs;
        objectsICollideWith = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shockwave>().objectsShockwaveCollidesWith;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsICollideWith.Add(other.gameObject);
        Debug.Log("Triggered object: " + other.gameObject.name);
    }
}
