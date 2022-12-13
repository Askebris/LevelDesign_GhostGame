using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI Grenade;
    public static ExplosiveController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject explosivePrefab;
    float fireTime;
    float fireRate = 1f;
    public float currentGrenades;
    public float maxGrenades = 5f;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        currentGrenades = maxGrenades;
    }
    
    
    public void PlayerExplosive()
    {
        if (Time.time - fireRate < fireTime) return;

        if (currentGrenades <= 0) return;

        currentGrenades--;
        fireTime = Time.time;
        var newExplosive = Instantiate(explosivePrefab, spawnPoint.position, spawnPoint.rotation); //spawn on player position
    }
    public void GrenadePickup()
    {
        currentGrenades = maxGrenades; //called/added automatically maybe on pickup
    }
    
    
    public void OnGrenade()
    {
        if (currentGrenades > 0)
        {
            audioManager.Play("grenadebeep");
        }
        PlayerExplosive();
        
    }

}
