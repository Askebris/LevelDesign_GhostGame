using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRapidfire : MonoBehaviour
{
    PlayerAmmo playerAmmo;
    GunController gunController;
    AudioManager audioManager;
    [SerializeField] public Image infiniteloop;
    [SerializeField] public Image rapidFire;
    [SerializeField] TextMeshProUGUI Ammo;
    [SerializeField] public Image ClockEnable;
    [SerializeField] public Image ClockTimeEnable;
    [SerializeField] TextMeshProUGUI SecondsEnable;
    [SerializeField] private float rapidFireTime;
    [SerializeField] private Image clock;
    [SerializeField] private TextMeshProUGUI seconds;
    private float currentTime;

    public void Update()
    {
        if (playerAmmo.currentAmmo > 50)
        {
            gunController.OnShoot();
            currentTime += Time.deltaTime;
            clock.fillAmount = 1 - (currentTime / rapidFireTime);
            seconds.text = Mathf.RoundToInt(currentTime).ToString();
        }
    }
    private void Awake()
    {
        playerAmmo = FindObjectOfType<PlayerAmmo>();
        gunController = FindObjectOfType<GunController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Rapidfire()
    {
        gunController.fireRate = 0.01f;
        ClockEnable.enabled = true;
        ClockTimeEnable.enabled = true;
        SecondsEnable.enabled = true;
        playerAmmo.currentAmmo = 999;
        infiniteloop.enabled = true;
        Ammo.enabled = false;
        rapidFire.enabled = true;
        audioManager.Play("rapidfire");
        StartCoroutine(RemoveAfterSeconds(0.5f));
        StartCoroutine(RemoveRapidFire(rapidFireTime));
    }

    IEnumerator RemoveRapidFire(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gunController.fireRate = 0.2f;
        infiniteloop.enabled = false;
        Ammo.enabled = true;
        ClockEnable.enabled = false;
        ClockTimeEnable.enabled = false;
        SecondsEnable.enabled = false;
        playerAmmo.currentAmmo = playerAmmo.maxAmmo;
        playerAmmo.Ammo.text = Mathf.RoundToInt(playerAmmo.currentAmmo).ToString();
    }

    IEnumerator RemoveAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = false;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = true;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = false;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = true;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = false;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = true;
        yield return new WaitForSeconds(seconds);
        rapidFire.enabled = false;
    }


}
