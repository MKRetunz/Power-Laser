using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GameObject gun;
    public Animator shootanim;
    public bool shooting;
    public bool ADS;

	// Use this for initialization
	void Start () {
        shooting = false;
        ADS = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Shooting mechanics
        if (Input.GetMouseButtonDown(0))
        {
            if (!ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("Gun_Shoot");
                //shooting = true;
            }
            else if (ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("GunADS_Shoot");
                //shooting = true;
            }
        }

        //Aim mechanics
        if (Input.GetMouseButtonDown(1))
        {
            if (!ADS && !shooting)
            {
                Debug.Log("Pressed right click.");
                gun.GetComponent<Animator>().Play("GunToADS");
                ADS = true;
            } 
            else if (ADS && !shooting)
            {
                Debug.Log("Pressed right click.");
                gun.GetComponent<Animator>().Play("GunFromADS");
                ADS = false;
            }
        }

    }
}
