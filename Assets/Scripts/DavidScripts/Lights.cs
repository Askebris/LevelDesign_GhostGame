using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour

{
    [SerializeField] GameObject LightObj;
    private bool PlayerInZone;
    private bool EnemyInZone;
    private bool LightIsOn ;//check if the player is in trigger

    // Start is called before the first frame update
    void Start()
    {
        PlayerInZone = false;
        EnemyInZone = false;
        LightIsOn = false;     
    }
    private void Update()
    {
        if (PlayerInZone && (LightIsOn == false))          
        {
            Debug.Log("Playa torn on da lights");
            LightObj.SetActive(true);

            LightIsOn = true;
            

        }
        if (EnemyInZone && (LightIsOn == true))         
        {
            Debug.Log("Enemy turn on lights");
            LightObj.SetActive(false);

            LightIsOn = false;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")     //if player in zone
        {
            Debug.Log("Player in da zone");
            PlayerInZone = true;
        }
        if (other.gameObject.tag == "Enemy")     //if Enemy in zone
        {
            Debug.Log("Enemy in da zone");
            EnemyInZone = true;
        }
    }


    private void OnTriggerExit(Collider other)     
    {
        if (other.gameObject.tag == "Player")    //if player exit zone
        {
            PlayerInZone = false;

        }
        if (other.gameObject.tag == "Enemy")     //if Enemy exit zone
        {
            EnemyInZone = false;

        }
    }
}
