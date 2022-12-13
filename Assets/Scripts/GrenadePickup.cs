using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    ExplosiveController playerGrenade;
    private AudioManager audioManager;

    private void Awake()
    {
        playerGrenade = FindObjectOfType<ExplosiveController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (playerGrenade.currentGrenades < playerGrenade.maxGrenades)
        {
            playerGrenade.GrenadePickup();
            audioManager.Play("pickup2");
            Destroy(gameObject); 
        }
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
