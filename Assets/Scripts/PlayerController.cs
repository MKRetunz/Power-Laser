using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public static bool shooting;
    public static bool ADS;
    public static bool switchADS;
    private float shootDelay;

    // Use this for initialization
    void Start()
    {
        shootDelay = 0;
        shooting = false;
        switchADS = false;
        ADS = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.F))
        {
            HUD.enemyDie = true;
            HUD.score += 80;
        }*/

        // Shoot delay
        if(shooting && shootDelay < 0.8f)
        {
            shootDelay += 1 * Time.deltaTime;
        }
        else if(shooting && shootDelay >= 0.8f)
        {
            shootDelay = 0;
            shooting = false;
        }

        //Shooting mechanics
        if (Input.GetMouseButtonDown(0) && !shooting && !switchADS)
        {
            if (!ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("Gun_Shoot");
            }
            else if (ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("GunADS_Shoot");
            }
        }

        //Aim mechanics
        if (Input.GetMouseButtonDown(1))
        {
            if (!ADS && !shooting && !switchADS)
            {
                Debug.Log("Pressed right click.");
                gun.GetComponent<Animator>().Play("GunToADS");
                switchADS = true;
                ADS = true;
            }
            else if (ADS && !shooting && !switchADS)
            {
                Debug.Log("Pressed right click.");
                gun.GetComponent<Animator>().Play("GunFromADS");
                switchADS = true;
                ADS = false;
            }
        }

    }
}
