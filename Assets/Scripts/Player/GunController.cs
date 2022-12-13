using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    Transform enemy;
    private @InputActionsMap inputActionsMap;
    private AudioManager audioManager;
    private EnemyBehaviour enemyScript;
    public static GunController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float shootSpeed;
    float damage = 1f;
    public float flashLightRate = 0.2f;
    public Light spotlight;
    public float viewDistance;
    private float viewAngle;
    public LayerMask viewMask;
    Color originalSpotlightColor = Color.yellow;
    public float timeToKillEnemy;
    private float enemyDeadTimer;

    private void Awake()
    {
        inputActionsMap = new @InputActionsMap();
        audioManager = FindObjectOfType<AudioManager>();
        enemyScript = FindObjectOfType<EnemyBehaviour>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        inputActionsMap.Enable();
    }
    private void OnDisable()
    {
        inputActionsMap.Disable();
    }

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        viewAngle = spotlight.spotAngle;
        spotlight.color = originalSpotlightColor;
    }
    void Update()
    {
          
        if (inputActionsMap.Player.Shoot.triggered)
        {
            inputActionsMap.Player.Shoot.started += DrainBattery;
            inputActionsMap.Player.Shoot.started += TurnOn;
            inputActionsMap.Player.Shoot.canceled += TurnOff;
        }

        if (CanSeeEnemy() && spotlight.enabled == true)
        {
            Debug.Log("DIE Ghost!");
            enemyScript.enemyTakeDamage = true;
            //enemyDeadTimer += Time.deltaTime;
            enemyScript.TakeDamage(damage);
        }
        else
        {
            enemyScript.enemyTakeDamage = false;
            //Debug.Log("Player: Where r u?");
        }
 
    }

    private void TurnOff(InputAction.CallbackContext context)
    {
        //Debug.Log("Flashlight off!");
        spotlight.enabled = false;
    }
    private void TurnOn(InputAction.CallbackContext context)
    {
        //Debug.Log("Flashlight On!");
        spotlight.enabled = true;
    }

    private void DrainBattery(InputAction.CallbackContext context)
    {
        //StartCoroutine(BatteryTimer());
        //Debug.Log("Draining battery");
    }

    public void OnShoot()
    {
        if (spotlight.enabled == false) //&& playerAmmo.currentAmmo > 0
        {
            //Debug.Log("Flashlight On!");
            audioManager.Play("shoot");
        }
        else
        {
            //Debug.Log("Get Battery!");
            audioManager.Play("noammo");
        }
        //PlayerFire();
    }
    bool CanSeeEnemy()
    {
        if (Vector3.Distance(transform.position, enemy.position) < viewDistance)
        {
            Vector3 dirToEnemy = (enemy.position - transform.position).normalized;
            float angleBetweenPlayerAndEnemy = Vector3.Angle(transform.forward, dirToEnemy);
            if (angleBetweenPlayerAndEnemy < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, enemy.position, viewMask))
                {
                    //Debug.Log("true");
                    return true;
                }
            }
            //Debug.Log("false");
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        // Red line that shows flashlight view distance
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
