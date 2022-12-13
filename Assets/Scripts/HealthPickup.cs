using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHealth playerHealth;
    AudioManager audioManager;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
            audioManager.Play("healthpickup");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
