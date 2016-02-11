using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairSprite;
    public Rect position;
    public float scaleTrigger = 0.1f;
    public float scaleDelay = 0;
    public static bool crosshairTrigger = true;
    public bool scaleToADS = false;
    public bool scaleFromADS = false;

    void Start()
    {
        position = new Rect((Screen.width - crosshairSprite.width / 2) / 2, (Screen.height - crosshairSprite.height / 2) / 2, crosshairSprite.width / 2, crosshairSprite.height / 2);
    }

    void OnGUI()
    {
        //Run/ADS/Idle controller
        if (!PlayerController.ADS && Input.GetMouseButtonDown(1))
        {
            scaleToADS = true;
        }
        else if (PlayerController.ADS && Input.GetMouseButtonDown(1))
        {
            scaleFromADS = true;
        }

        if (crosshairTrigger)
        {
            GUI.DrawTexture(position, crosshairSprite);
        }
    }

    void Update()
    {
        //toADS
        if (scaleToADS && scaleDelay < 0.2 && !scaleFromADS)
        {
            Debug.Log("Count");
            scaleTrigger += 1.8f;
            scaleDelay += 1 * Time.deltaTime;
            position = new Rect((Screen.width - crosshairSprite.width / 2 + scaleTrigger) / 2, (Screen.height - crosshairSprite.height / 2 + scaleTrigger) / 2, crosshairSprite.width / 2 - scaleTrigger, crosshairSprite.height / 2 - scaleTrigger);
        }
        if (scaleDelay >= 0.2 && scaleToADS && !scaleFromADS)
        {
            scaleTrigger = 0;
            scaleToADS = false;
            scaleDelay = 0;
            crosshairTrigger = false;
        }

        //fromADS
        if (scaleFromADS && scaleDelay < 0.2 && !scaleToADS)
        {
            scaleTrigger += 1.8f;
            scaleDelay += 1 * Time.deltaTime;
            crosshairTrigger = true;
            position.x = (Screen.width - crosshairSprite.width / 2 - scaleTrigger) / 2;
            position.y = (Screen.width - crosshairSprite.width / 2 - scaleTrigger) / 2;
            position.width += scaleTrigger;
            position.height += scaleTrigger;
        }
        if (scaleFromADS && !scaleToADS && scaleDelay >= 0.2)
        {
            scaleTrigger = 0;
            scaleFromADS = false;
            scaleDelay = 0;
        }
    }
}