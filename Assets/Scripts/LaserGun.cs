using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class LaserGun : MonoBehaviour
{
    public Camera c;
    public GameObject laserparticles;
    public Transform particlerotation;

    LineRenderer line;
    Vector3 shotPoint;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        shotPoint = transform.position;
        if (Input.GetMouseButtonDown(0)) {
            StopCoroutine("ShootLaser");
            StartCoroutine("ShootLaser");
        }
    }

    IEnumerator ShootLaser()
    {
        line.enabled = true;

        while(Input.GetMouseButtonDown(0))
        {
            line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);
            
            Ray ray = new Ray();
            ray = c.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            line.SetPosition(0, shotPoint);

            if(Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                if(hit.rigidbody && !hit.transform.GetComponent<TargetScript>().isAlive)
                {
                    hit.rigidbody.AddForceAtPosition(transform.forward * 1000, hit.point);
                } else if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(transform.forward * 150, hit.point);
                    hit.transform.GetComponent<TargetScript>().hp--;
                }
                Instantiate(laserparticles, hit.point, particlerotation.transform.rotation);
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;
        }

        line.enabled = false;
    }
}
