using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //pistol
    public GameObject singleShotP;
    public GameObject burstShotP;
    public GameObject revolverP;

    //rifles
    public GameObject semiAutoR;
    public GameObject fullAutoR;
    public GameObject boltActionR;

    //Shotguns
    public GameObject pumpActionS;
    public GameObject semiAutoS;

    //array
    private GameObject[] gunArray;
    
    //Bools
    public static bool shooting;
    public static bool ADS;
    public static bool switchADS;
    public static bool OverHeat;
    private bool CanCover;
    private bool Covering;
    private bool showText;
    private bool rapidFire;
    
    //Floats and ints
    public float crouchingSpeed;
    public float CspeedUp;
    public float GunHeat;
    public float TimerCover;
    public float hSliderValue = 0;
    private float shootDelay;
    public float fireRate;
    private float PowerUpTimer;

    public int currentGun;
    public int maxGuns;

    //UI
    public Slider HeatSlider;
    public Slider BoonSlider;
    public Text CoverPopUp;

    // Use this for initialization
    void Start()
    {
        shootDelay = 0;
        TimerCover = 0.0f;
        crouchingSpeed = 0.1f;
        CspeedUp = crouchingSpeed;
        GunHeat = 0.0f;
        PowerUpTimer = 0.0f;

        currentGun = 0;
        maxGuns = 8;
     
        showText = false;
        shooting = false;
        switchADS = false;
        ADS = false;
        rapidFire = false;

        gunArray = new GameObject[maxGuns];

        gunArray[0] = singleShotP;
        gunArray[1] = burstShotP;
        gunArray[2] = revolverP;
        gunArray[3] = semiAutoR;
        gunArray[4] = fullAutoR;
        gunArray[5] = boltActionR;
        gunArray[6] = pumpActionS;
        gunArray[7] = semiAutoS;

        for (int i = 1; i < maxGuns; i++)
        {
            gunArray[i].SetActive(false);

        }

        rapidFire = true;
        PowerUpTimer = 30.0f;
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

        if (col.GetComponent<Collider>().name == "FirePickUp")
        {
            rapidFire = true;
            PowerUpTimer = 30.0f;
            Destroy(col.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot delay
        if (shooting && shootDelay < fireRate)
        {
            shootDelay += 1 * Time.deltaTime;
        }
        else if (shooting && shootDelay >= fireRate)
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
                //singleShotP.GetComponent<Animator>().Play("Gun_Shoot");
            }
            else if (ADS)
            {
                Debug.Log("Pressed left click.");
                //singleShotP.GetComponent<Animator>().Play("GunADS_Shoot");
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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchGun(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchGun(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchGun(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            switchGun(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            switchGun(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            switchGun(7);
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


        if (PowerUpTimer <= 0.0f && rapidFire == true)
        {
            PowerUpTimer += Time.deltaTime;
            fireRate = 0.1f;
        }

        if (PowerUpTimer >= 30.0f)
        {
            rapidFire = false;
            resetGun(currentGun);
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
        BoonSlider.value = PowerUpTimer;

        //Aim mechanics
        if (Input.GetMouseButtonDown(1))
        {
            if (!ADS && !shooting && !switchADS)
            {
                Debug.Log("Pressed right click.");
                singleShotP.GetComponent<Animator>().Play("GunToADS");
                switchADS = true;
                ADS = true;
            }
            else if (ADS && !shooting && !switchADS)
            {
                Debug.Log("Pressed right click.");
                singleShotP.GetComponent<Animator>().Play("GunFromADS");
                switchADS = true;
                ADS = false;
            }
        }
    }

    //Weapon changing
    void switchGun(int newWeapon)
    {
        for (int i = 0; i < maxGuns; i++)
        {
            gunArray[i].SetActive(false);

            if(i == newWeapon)
            {
                gunArray[i].SetActive(true);
                currentGun = i;
            }
        }        
    }

    void resetGun(int CG)
    {
        for (int i = 0; i < maxGuns; i++)
            {
                gunArray[i].SetActive(false);
        
            if (i == CG)
            {
                gunArray[i].SetActive(true);
            }
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
