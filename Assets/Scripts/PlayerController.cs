using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Guns
    public GameObject gun;
    public GameObject rifle;
    
    //Bools
    public static bool shooting;
    public static bool ADS;
    public static bool switchADS;
    public static bool OverHeat;
    private bool CanCover;
    private bool Covering;
    private bool showText;
    
    //Floats and ints
    public float crouchingSpeed;
    public float CspeedUp;
    public float GunHeat;
    public float TimerCover;
    public float hSliderValue = 0;
    private float shootDelay;
    public float currentGun;

    //UI
    public Slider HeatSlider;
    public Text CoverPopUp;

    // Use this for initialization
    void Start()
    {
        shootDelay = 0;
        TimerCover = 0.0f;
        crouchingSpeed = 0.1f;
        CspeedUp = crouchingSpeed;
        GunHeat = 0.0f;
        currentGun = 0f;
     
        showText = false;
        shooting = false;
        switchADS = false;
        ADS = false;

        rifle.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().name == "Cover")
        {
            Debug.Log("I'm Working");
            showText = true;
            TimerCover = 0.0f;
            CanCover = true;
        }
        if (col.GetComponent<Collider>().name == "Uncover")
        {
            CanCover = false;
        }
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
        if (shooting && shootDelay < 0.8f)
        {
            shootDelay += 1 * Time.deltaTime;
        }
        else if (shooting && shootDelay >= 0.8f)
        {
            shootDelay = 0;
            shooting = false;
        }

        //Shooting mechanics
        if (Input.GetMouseButtonDown(0) && !shooting && !switchADS && !OverHeat)
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
            GunHeat += Time.deltaTime * 20;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchGun(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchGun(1);
        }

        //Crouching
        if (Input.GetKey("x"))
        {
            CspeedUp -= crouchingSpeed * (Time.deltaTime * 10);
            if (CspeedUp < -0.6f)
            {
                CspeedUp = -0.6f;
            }
        }

        //Cover
        else if (CanCover == true && Input.GetKey("z"))
        {
            CspeedUp = -0.6f;
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
        GunHeat -= Time.deltaTime / 2;
        TimerCover += Time.deltaTime;

        if (TimerCover > 3.0)
        {
            showText = false;
        }

        if (GunHeat < 0.0f)
        {
            GunHeat = 0.0f;
            OverHeat = false;
        }
        if (GunHeat > 1.5f) { OverHeat = true; }
        HeatSlider.value = GunHeat;

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

    //Weapon changing
    void switchGun(int newWeapon)
    {
        //Add an Array
        if(newWeapon == 0)
        {
            gun.SetActive(false);
            rifle.SetActive(true);
            currentGun = 1f;
        }
        if(newWeapon == 1)
        {
            gun.SetActive(true);
            rifle.SetActive(false);
            currentGun = 0f;
        }
    }

    void OnGUI()
    {
        if (showText)
        {
            GUIStyle Coverstyle = new GUIStyle();
            Coverstyle.alignment = TextAnchor.MiddleCenter;
            Coverstyle.fontSize = 50;
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 40, 400, 30), "Hold Z for cover.", Coverstyle);
        }
    }
}
