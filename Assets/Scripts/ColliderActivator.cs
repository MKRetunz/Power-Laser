using UnityEngine;
using System.Collections;

public class ColliderActivator : MonoBehaviour {

    float timer = 0.0f;
    bool down = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

	    if (timer >= 1.5f && down == false)
        {
            transform.localPosition = new Vector3(7.5f, -5, 0);

            down = true;

        }
        if (timer >= 3.0f && down == true)
        {
            transform.localPosition = new Vector3(7.5f, 0, 0);

            down = false;

            timer = 0.0f;
        }
	}
}
