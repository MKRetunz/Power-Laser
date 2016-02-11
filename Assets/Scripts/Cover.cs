using UnityEngine;
using System.Collections;

public class Cover : MonoBehaviour
{
    private bool CanCover;

    void Start()
    {
        CanCover = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().name == "Cover")
        {
            Debug.Log("I'm Working");
            CanCover = true;
        }
    }

    void Update()
    {
        if (CanCover == true && Input.GetKeyDown("l"))
        {
            Debug.Log("Still working");
            CanCover = false;
        }
    }
}