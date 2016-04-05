using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    float timer;
    public float targetTime = 3.0f;
    public Rigidbody blueprint;

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= targetTime)
        {
            Rigidbody enemy = Instantiate(blueprint, transform.position, transform.rotation) as Rigidbody;
            timer = 0.0f;
        }

    }
}
