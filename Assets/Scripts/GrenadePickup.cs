using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    ExplosiveController playerGrenade;
    private AudioManager audioManager;
    private ScoreManager scoreManager;

    private void Awake()
    {
        playerGrenade = FindObjectOfType<ExplosiveController>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (playerGrenade.currentGrenades < playerGrenade.maxGrenades)
        {
            scoreManager.scorePoints += 100;
            scoreManager.Score.text = "" + Mathf.RoundToInt(scoreManager.scorePoints).ToString();
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
