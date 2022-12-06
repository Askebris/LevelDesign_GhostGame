using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehaviour : MonoBehaviour
{
    private ScoreManager scoreManager;
    private AudioManager audioManager;
    private PlayerHealth playerHealth;
    [Header("Follow Player")]
    private GameObject myTarget;
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private int range;
    [SerializeField] public int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [Header("Health")]
    [SerializeField] private float health = 5f;
    private bool canAttack = true;

    private void Awake()
    {
        myTarget = GameObject.FindWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, myTarget.transform.position);

        if (dist < range)
        {
            myAgent.destination = myTarget.transform.position;
        }

        if (dist <= attackRange && canAttack)
        {
            playerHealth.PlayerTakeDamage(damage);
            StartCoroutine(AttackTimer());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        scoreManager.scorePoints += 2;
        scoreManager.Score.text = "" + Mathf.RoundToInt(scoreManager.scorePoints).ToString();
        if (health <= 0)
        {
            audioManager.Play("ghostdie");
            scoreManager.scorePoints += 10;
            scoreManager.Score.text = "" + Mathf.RoundToInt(scoreManager.scorePoints).ToString();
            Destroy(gameObject);
        }
    }

    private IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
        if (playerHealth.currentHealth > 0)
        {
            audioManager.Play("ghostattack");
        }
        
    }
}
