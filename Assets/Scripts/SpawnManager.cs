using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private float waveAmount = 0;
    private float waveIncrease = 1;
    private float waveStartAmount = 2;
    private AudioManager audioManager;

    [SerializeField] private GameObject enemy1;
    private GameObject newEnemy;

    public List<GameObject> enemyList;

    
    private bool readyToSpawn = true;
    [SerializeField] private float waveCD;
    
    private void Awake()
    {
        enemyList = new List<GameObject>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    

    private void SpawnWave()
    {
        waveAmount = waveStartAmount + waveIncrease;
        for (int i = 0; i < waveAmount; i++)
        {
            newEnemy = Instantiate(enemy1, transform.position, Quaternion.identity);
            
            enemyList.Add(newEnemy);
        }
        waveIncrease += 1;
    }

    private void Update()
    {
        if (readyToSpawn)
        {
            audioManager.Play("spawnzombie");
            SpawnWave();
            readyToSpawn = false;
            
            Invoke(nameof(ResetSpawn), waveCD + waveIncrease);
        }
    }

    private void ResetSpawn()
    {
        readyToSpawn = true;
    }
}
