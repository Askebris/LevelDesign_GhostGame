using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInCone : MonoBehaviour

{
    public bool enemyInDaCone;
    private bool LightIsOn ;//check if the player is in trigger

    // Start is called before the first frame update
    void Start()
    {
        enemyInDaCone = false;
        LightIsOn = false;     
    }
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")     //if Enemy in zone
        {
            //Debug.Log("Enemy in da CONE");
            enemyInDaCone = true;
        }
    }


    private void OnTriggerExit(Collider other)     
    {
        if (other.gameObject.tag == "Enemy")     //if Enemy exit zone
        {
            enemyInDaCone = false;
        }
    }
}
