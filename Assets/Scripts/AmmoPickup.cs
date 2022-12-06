using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoPickup : MonoBehaviour
{
    private PlayerAmmo playerAmmo;
    private AudioManager audioManager;
    private ScoreManager scoreManager;
    private void Awake()
    {
        playerAmmo = FindObjectOfType<PlayerAmmo>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (playerAmmo.currentAmmo < playerAmmo.maxAmmo)
        {
            playerAmmo.currentAmmo = playerAmmo.maxAmmo;
            playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
            scoreManager.scorePoints += 100;
            scoreManager.Score.text = "" + Mathf.RoundToInt(scoreManager.scorePoints).ToString();
            audioManager.Play("ammopickup");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
