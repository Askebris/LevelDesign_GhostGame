using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] public TextMeshProUGUI Score;
    [SerializeField] public TextMeshProUGUI HighScore;
    public int scorePoints = 0;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Score.text = "" + Mathf.RoundToInt(scorePoints).ToString();
    }

    void Update()
    {

    }

    public void AddPoints(int pointsToAdd)
    {
        scorePoints += pointsToAdd;
        Score.text = "" + Mathf.RoundToInt(scorePoints).ToString();
    }

    public void Reset()
    {
        Score.text = Mathf.RoundToInt(0).ToString();
    }
}
