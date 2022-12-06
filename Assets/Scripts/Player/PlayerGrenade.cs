using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Grenade;
    public int grenades;
    public int maxGrenades = 3;
    public int currentGrenades;
    // Start is called before the first frame update

    private void Start()
    {
        currentGrenades = 1;
    }
    // Update is called once per frame
    void Update()
    {
        Grenade.text = Mathf.RoundToInt(currentGrenades).ToString();
        if (Input.GetKeyDown(KeyCode.G) && currentGrenades > 0)
        {
            currentGrenades--;
            grenades = currentGrenades;
        }
    }
}
