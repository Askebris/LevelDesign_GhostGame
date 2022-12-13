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
    private AudioManager audioManager;
    private PlayerHealth playerHealth;
    private enemyChangeColor enemyColor;
    [Header("Follow Player")]
    private GameObject myTarget;
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private int range;
    [SerializeField] public int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [Header("Health")]
    [SerializeField] public float health = 100f;
    private bool canAttack = true;
    public bool enemyTakeDamage = false;
    public Transform enemy;
    private void Awake()
    {
        enemyTakeDamage = false;
        myTarget = GameObject.FindWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
        enemyColor = FindObjectOfType<enemyChangeColor>(); 
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
        Debug.Log("Taking DAMAGE!");
        health -= damage;
        enemyColor.altColor.g += 0.1f;

        if (health <= 0)
        {
            audioManager.Play("ghostdie");
            Destroy(gameObject);
            enemyTakeDamage = false;
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
