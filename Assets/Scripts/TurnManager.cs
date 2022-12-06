using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    private AudioManager audioManager;
    private ScoreManager scoreManager;
    [SerializeField] private float GameTime;
    [SerializeField] private TextMeshProUGUI timeText;
    public CanvasGroup WinTheGameCanvasGroup;
    private int m_score;
    private int m_highscore;
    public float fadeDuration = 0.1f;
    public float displayImageDuration = 7f;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        DisplayTime(GameTime);
        GameTime -= Time.deltaTime;
        if (GameTime <= 0)
        {
            m_score = Convert.ToInt32(scoreManager.Score.text);
            m_highscore = Convert.ToInt32(scoreManager.HighScore.text);
            StartCoroutine(EndLevel(WinTheGameCanvasGroup));
            if (m_highscore < m_score)
            {
                    scoreManager.HighScore.text = scoreManager.Score.text;
            }
        }    
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private IEnumerator EndLevel(CanvasGroup imageCanvasGroup)
        {
            audioManager.Stop("theme");
            audioManager.Stop("nightambience");
            imageCanvasGroup.alpha = 1;
            yield return new WaitForSeconds(fadeDuration);
            yield return new WaitForSeconds(displayImageDuration);
            SceneManager.LoadScene("SampleScene");
        }
    }


