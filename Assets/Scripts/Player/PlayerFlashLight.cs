using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
public class PlayerFlashLight : MonoBehaviour
{
    private AudioManager audioManager;
    public static GunController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootSpeed;
    float fireTime;
    public float fireRate = 0.2f;
    public Light spotlight;
    public float viewDistance;
    private float viewAngle;

    public float timeToKillEnemy;
    private float enemyDeadTimer;

    //float damage = 5f;

    public LayerMask viewMask;
    Transform enemy;
    Color originalSpotlightColor = Color.yellow;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        viewAngle = spotlight.spotAngle;
        spotlight.color = originalSpotlightColor;
    }

    void Update()
    {
        if (CanSeeEnemy())
        {
            Debug.Log("Player: I see you Ghost!");
            enemyDeadTimer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Player: Where r u?");
            enemyDeadTimer -= Time.deltaTime;
        }
        enemyDeadTimer = Mathf.Clamp(enemyDeadTimer, 0, timeToKillEnemy);
        // Fade between idle color to spotted color of spotlight depending on playerVisibleTimer/timeToSpotPlayer;
        spotlight.color = Color.Lerp(originalSpotlightColor, Color.red, enemyDeadTimer / timeToKillEnemy);
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
    private void OnDrawGizmos()
    {
        // Red line that shows flashlight view distance
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
    public void PlayerFire()
    {
        if (Time.time - fireRate < fireTime) return;

        fireTime = Time.time;
        var newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootSpeed, ForceMode.Impulse);
    }
    public void OnShoot()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        PlayerFire();
    }
}
