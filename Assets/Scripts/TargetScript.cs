using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {
    public byte hp;
    public bool isAlive;
    public Transform target;

	// Use this for initialization
	void Start () {
        hp = 3;
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(hp <= 0)
        {
            isAlive = false;
        }

        if(isAlive)
        {
            transform.LookAt(target);
        }
	}
}
