using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairSprite;
    public Rect position;
    public float scaleTrigger = 0.1f;
    public int scaleDelay = 0;
    public static bool crosshairTrigger = true;
    public bool scaling = false;

    void Start()
    {
        position = new Rect((Screen.width - crosshairSprite.width / 2) / 2, (Screen.height - crosshairSprite.height / 2) / 2, crosshairSprite.width / 2, crosshairSprite.height / 2);
    }

    void OnGUI()
    {
        //Run/ADS/Idle controller
        if (!PlayerController.ADS && Input.GetMouseButtonDown(1))
        {
            scaling = true;
        }
        else if (PlayerController.ADS && Input.GetMouseButtonDown(1))
        {

        }

        if (crosshairTrigger)
        {
            GUI.DrawTexture(position, crosshairSprite);
        }
    }

    void Update() { 
        if(scaling && scaleDelay < 100)
        {
            scaleTrigger += 0.1f;
            scaleDelay++;
            position = new Rect((Screen.width - crosshairSprite.width / 2) / 2 - scaleTrigger, (Screen.height - crosshairSprite.height / 2) / 2 + scaleTrigger, crosshairSprite.width / 2 + scaleTrigger, crosshairSprite.height / 2 - scaleTrigger);
        }
        if (scaleDelay >= 100 && scaling)
        {
            scaling = false;
            scaleDelay = 0;
            crosshairTrigger = false;
        }
    }
}