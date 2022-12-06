using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Ammo;
    public int ammo = 32;
    public int maxAmmo = 32;
    public int currentAmmo;
    // Start is called before the first frame update

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame



}
