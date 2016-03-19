using UnityEngine;
using System.Collections;

public class SelfDelete : MonoBehaviour {

    private float Timer;
    public float maxTime;

	// Use this for initialization
	void Start () {
        Timer = 0.0f;
        maxTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {

        Timer += Time.deltaTime;

        if (Timer >= maxTime && this.name == "Bullet(Clone)")
        {
            Destroy(this.gameObject);
        }
	}
}
