using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehaviour : MonoBehaviour
{
    private bool enemyInDaCone = false;
    private EnemyInCone enemyInCone;
    [SerializeField] GameObject Test;
    private GunController gunController;
    private AudioManager audioManager;
    private PlayerHealth playerHealth;
    //private enemyChangeColor enemyColor;
    [Header("Follow Player")]
    private GameObject myTarget;
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private int range;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [Header("Health")]
    [SerializeField] private float health;
    private bool canAttack = true;
    private void Awake()
    {
        enemyInCone = FindObjectOfType<EnemyInCone>();
        myTarget = GameObject.FindWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
        gunController = FindObjectOfType<GunController>();
        //enemyColor = FindObjectOfType<enemyChangeColor>(); 
    }

    private void Update()
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
        if (enemyInDaCone == true && gunController.spotlight.enabled == true)
        {
            Debug.Log("Soon Taking Damage");
            TakeDamage(1);
        }

        }

    public void TakeDamage(float damage)
    {
            Debug.Log("Taking DAMAGE!");
            this.health -= damage;
            //enemyColor.altColor.g += 0.1f;

            if (this.health <= 0)
            {
                Debug.Log("HAs DIED!");
                audioManager.Play("ghostdie");
                Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
                //enemyTakeDamage = false;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cone")     //if Enemy in zone
        {
            Debug.Log("Enemy in da CONE");
            enemyInDaCone = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cone")     //if Enemy exit zone
        {
            enemyInDaCone = false;
        }
    }
}
