using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    Transform enemy;
    private @InputActionsMap inputActionsMap;
    private PlayerAmmo playerAmmo;
    private AudioManager audioManager;
    private EnemyBehaviour enemyScript;
    public static GunController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float shootSpeed;
    //float flashLightTime;
    float damage = 1f;
    public float flashLightRate = 0.2f;
    //private float batteryDrainTime = 0.5f;
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
        playerAmmo = FindObjectOfType<PlayerAmmo>();
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
        playerAmmo.currentAmmo = playerAmmo.maxAmmo;
        enemyScript.enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        viewAngle = spotlight.spotAngle;
        spotlight.color = originalSpotlightColor;
        Reload();
    }
    void Update()
    {
        /*
        Vector2 aim = Gamepad.rightStick.ReadValue();
        Vector3 direction = new Vector3(aim.x, 0, aim.y); //if you're 2d side scroller, you need to swap 2nd and 3rd value.
        transform.rotation = Quaternion.LookRotation(direction);
        */
        
        if (inputActionsMap.Player.Shoot.triggered)
        {
            inputActionsMap.Player.Shoot.started += DrainBattery;
            inputActionsMap.Player.Shoot.started += TurnOn;
            inputActionsMap.Player.Shoot.canceled += TurnOff;
        }

        if (CanSeeEnemy() && spotlight.enabled == true)
        {
            enemyScript.enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
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
        /*
        enemyDeadTimer = Mathf.Clamp(enemyDeadTimer, 0, enemyScript.health);
        // Fade between original enemy color to dead enemy color depending on enemyDeadTimer/enemyScript.health;
        enemyColor.altColor = Color.Lerp(enemyColor.originalEnemyColor, enemyColor.deadEnemyColor, enemyDeadTimer / enemyScript.health);
        enemyColor.rend.material.color = enemyColor.altColor;
        */
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

    /*
    public void PlayerFire()
    {
        if (Time.time - flashLightRate < flashLightTime) return;

        if (playerAmmo.currentAmmo <= 0) return;

        playerAmmo.currentAmmo--;
        playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
        flashLightTime = Time.time;
    }
    */
    public void Reload()
    {
        playerAmmo.currentAmmo = playerAmmo.maxAmmo; //picked up ammo?
    }


    public void OnShoot()
    {
        //enemyScript.enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
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
        if (Vector3.Distance(transform.position, enemyScript.enemy.position) < viewDistance)
        {
            Vector3 dirToEnemy = (enemyScript.enemy.position - transform.position).normalized;
            float angleBetweenPlayerAndEnemy = Vector3.Angle(transform.forward, dirToEnemy);
            if (angleBetweenPlayerAndEnemy < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, enemyScript.enemy.position, viewMask))
                {
                    //Debug.Log("true");
                    return true;
                }
            }
            //Debug.Log("false");
        }
        return false;
    }
  
    /*
    private IEnumerator BatteryTimer()
    {
        playerAmmo.currentAmmo--;
        playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
        yield return new WaitForSeconds(batteryDrainTime);
        playerAmmo.currentAmmo--;
        playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
        
        if (playerAmmo.currentAmmo > 0)
        {
            //audioManager.Play("ghostattack");
        }

    }
    */
    private void OnDrawGizmos()
    {
        // Red line that shows flashlight view distance
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
