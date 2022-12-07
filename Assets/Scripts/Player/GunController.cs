using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private @InputActionsMap inputActionsMap;
    private PlayerAmmo playerAmmo;
    private AudioManager audioManager;
    public static GunController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float shootSpeed;
    float flashLightTime;
    float damage = 25f;
    public float flashLightRate = 0.2f;
    private float batteryDrainTime = 0.5f;
    public Light spotlight;
    public float viewDistance;
    private float viewAngle;
    public LayerMask viewMask;
    Transform enemy;
    Color originalSpotlightColor = Color.yellow;
    public float timeToKillEnemy;
    private float enemyDeadTimer;

    private void Awake()
    {
 
        inputActionsMap = new @InputActionsMap();
        audioManager = FindObjectOfType<AudioManager>();
        playerAmmo = FindObjectOfType<PlayerAmmo>();

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
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        viewAngle = spotlight.spotAngle;
        spotlight.color = originalSpotlightColor;
        Reload();
    }
    void Update()
    {
        if(inputActionsMap.Player.Shoot.triggered)
        {
            inputActionsMap.Player.Shoot.started += DrainBattery;
            inputActionsMap.Player.Shoot.canceled += TurnOff;
        }

        if (CanSeeEnemy())
        {
            //Debug.Log("Player: I see you Ghost!");
            enemyDeadTimer += Time.deltaTime;
            gameObject.GetComponentInParent<EnemyBehaviour>().TakeDamage(damage);
        }
        else
        {
            //Debug.Log("Player: Where r u?");
            enemyDeadTimer -= Time.deltaTime;
        }
        enemyDeadTimer = Mathf.Clamp(enemyDeadTimer, 0, timeToKillEnemy);
        // Fade between idle color to spotted color of spotlight depending on playerVisibleTimer/timeToSpotPlayer;
        spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, enemyDeadTimer / timeToKillEnemy);
    }

    private void TurnOff(InputAction.CallbackContext context)
    {
        Debug.Log("Flashlight off!");
        spotlight.enabled = false;
    }

    private void DrainBattery(InputAction.CallbackContext context)
    {
        //StartCoroutine(BatteryTimer());
        Debug.Log("Draining battery");
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
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        if (playerAmmo.currentAmmo > 0)
        {
            Debug.Log("Flashlight On!");
            spotlight.enabled = true;
            audioManager.Play("shoot");
        }
        else
        {
            Debug.Log("Get Battery!");
            spotlight.enabled = false;
            audioManager.Play("noammo");
        }
        //PlayerFire();
    }
    bool CanSeeEnemy()
    {
        if (Vector3.Distance(transform.position, enemy.position) < viewDistance)
        {
            //GetComponentInParent<EnemyBehaviour>().TakeDamage(damage);
            Vector3 dirToEnemy = (enemy.position - transform.position).normalized;
            float angleBetweenPlayerAndEnemy = Vector3.Angle(transform.forward, dirToEnemy);
            if (angleBetweenPlayerAndEnemy < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, enemy.position, viewMask))
                {
                    Debug.Log("true");
                    return true;
                }
            }
            Debug.Log("false");
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
