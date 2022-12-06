using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    AudioManager audioManager;
    ScoreManager scoreManager;
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
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            m_score = Convert.ToInt32(scoreManager.Score.text);
            m_highscore = Convert.ToInt32(scoreManager.HighScore.text);
            StartCoroutine(Died(LoseTheGameCanvasGroup));
            if (m_highscore < m_score)
            {
                scoreManager.HighScore.text = scoreManager.Score.text;
            }
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

    private IEnumerator Died(CanvasGroup imageCanvasGroup)
    {
        audioManager.Stop("theme");
        audioManager.Stop("nightambience");
        imageCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(fadeDuration);
        yield return new WaitForSeconds(displayImageDuration);
        SceneManager.LoadScene("SampleScene");
    }
}
