using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GameObject gun;
    public Animator shootanim;
    public static bool shooting;
    public static bool ADS;
    public float crouchingSpeed;
    public float CspeedUp;

	// Use this for initialization
	void Start () {
        shooting = false;
        ADS = false;
        crouchingSpeed = 0.1f;
        CspeedUp = crouchingSpeed;
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

        //Crouching
        if (Input.GetKey("k"))
        {
            CspeedUp -= crouchingSpeed * (Time.deltaTime * 10);
            if (CspeedUp < -0.6f)
            {
                CspeedUp = -0.6f;
            }
        }

        else
        {
            
            CspeedUp += crouchingSpeed * (Time.deltaTime * 10);
            if (CspeedUp > 0.0f)
            {
                CspeedUp = 0.0f;
            }
        }
        transform.localPosition = new Vector3(0, CspeedUp, 0);
        Debug.Log(CspeedUp);
    }
}
