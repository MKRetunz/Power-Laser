using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    float timer;
    public int wave;
    public int maxEnemies;
    bool waveactivator;
    int i;
    public float targetTime = 0.1f;
    public Rigidbody blueprint;

    // Use this for initialization
    void Start () {
        wave = 1;
        maxEnemies = 10;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().name == "Target(Clone)")
        {
            
        }

        else
        {
            newWave();
        }
    }

        // Update is called once per frame
        void Update () {

        timer += Time.deltaTime;

        if (timer >= targetTime && i < maxEnemies)
        {
            Rigidbody enemy = Instantiate(blueprint, transform.position, transform.rotation) as Rigidbody;
            timer = 0.0f;
            i++;

            waveactivator = true;
        }

        

    }

    void newWave ()
    {
        if (waveactivator)
        {
            wave++;
            i = 0;
            maxEnemies = wave * 10;
            waveactivator = false;
        }
    }
}
