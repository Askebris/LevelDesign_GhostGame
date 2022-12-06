using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private PlayerAmmo playerAmmo;
    private AudioManager audioManager;
    public static GunController instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootSpeed;
    float fireTime;
    public float fireRate = 0.2f;

    private void Awake()
    {
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
    
    private void Start()
    {
        Reload();
    }
    

    public void PlayerFire()
    {
        if (Time.time - fireRate < fireTime) return;

        if (playerAmmo.currentAmmo <= 0) return;

        playerAmmo.currentAmmo--;
        playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
        fireTime = Time.time;
        var newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootSpeed, ForceMode.Impulse);
    }
    public void Reload()
    {
        playerAmmo.currentAmmo = playerAmmo.maxAmmo; //picked up ammo?
    }


    public void OnShoot()
    {
        if (playerAmmo.currentAmmo > 0)
        {
            audioManager.Play("shoot");
        }
        else
        {
            audioManager.Play("noammo");
        }
        PlayerFire();
    }
}
