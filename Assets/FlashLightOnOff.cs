using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : MonoBehaviour
{
    public bool turnLightOn;                  //check if the player is in trigger
    public GameObject flashLightObj;

    public void Start()
    {
        turnLightOn = false;                   //player not in zone       
    }
    public void Update()
    {
        if (turnLightOn)           //if in zone and press F key
        {
            flashLightObj.SetActive(true);
        }

    }
    public void TurnOnFlashlight()
    {
        turnLightOn = true;
    }
    public void TurnOffFlashlight()
    {
        turnLightOn = false;
    }
}
