using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public Animator shootanim;
    public static bool shooting;
    public static bool ADS;

    // Use this for initialization
    void Start()
    {
        shooting = false;
        ADS = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting mechanics
        if (Input.GetMouseButtonDown(0))
        {
            if (!ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("Gun_Shoot");

                //Rigidbody instantiatedProjectile = Instantiate(bullet, GameObject.FindWithTag("shotpoint").transform.position, GameObject.FindWithTag("shotpoint").transform.rotation) as Rigidbody;
                //instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletspeed));

            }
            else if (ADS)
            {
                Debug.Log("Pressed left click.");
                gun.GetComponent<Animator>().Play("GunADS_Shoot");

               // Rigidbody instantiatedProjectile = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

                //instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletspeed));

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

    void Shoot()
    {

    }
}
