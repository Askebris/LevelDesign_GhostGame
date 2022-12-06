using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RapidFirePickup : MonoBehaviour
{
    private PlayerAmmo playerAmmo;
    private PlayerRapidfire playerRapidfire;
    private AudioManager audioManager;
    private ScoreManager scoreManager;
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void Awake()
    {
        playerRapidfire = FindObjectOfType<PlayerRapidfire>();
        playerAmmo = FindObjectOfType<PlayerAmmo>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (playerAmmo.currentAmmo <= playerAmmo.maxAmmo)
        {
            scoreManager.scorePoints += 100;
            scoreManager.Score.text = "" + Mathf.RoundToInt(scoreManager.scorePoints).ToString();
            playerRapidfire.Rapidfire();
            audioManager.Play("pickup1");
            Destroy(gameObject);
        }
    }
}
