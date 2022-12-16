using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] public HealthBar healthBar;
    public CanvasGroup LoseTheGameCanvasGroup;
    public int maxHealth = 100;
    public int currentHealth;
    public float fadeDuration = 0.1f;
    public float displayImageDuration = 7f;
    private int m_score;
    private int m_highscore;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            audioManager.Play("playerhurt3");
        }
    }
    /*
    private IEnumerator Died(CanvasGroup imageCanvasGroup)
    {
        audioManager.Stop("theme");
        audioManager.Stop("nightambience");
        imageCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(fadeDuration);
        yield return new WaitForSeconds(displayImageDuration);
        SceneManager.LoadScene("SampleScene");
    }
    */
}
