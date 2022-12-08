using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class enemyChangeColor : MonoBehaviour
{
    private EnemyBehaviour enemyScript;
    public UnityEngine.Color altColor = UnityEngine.Color.white;
    public Renderer rend;
    public UnityEngine.Color originalEnemyColor = UnityEngine.Color.white;
    public UnityEngine.Color deadEnemyColor = new UnityEngine.Color(0.5f, 0f, 0f, 1f);
    public float enemyDeadTimer;

    //I do not know why you need this?

    private void Awake()
    {
        enemyScript = FindObjectOfType<EnemyBehaviour>();
    }
    void Start()
    {
        //Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();
        //Set the initial color (0f,0f,0f,0f)
        rend.material.color = altColor;
    }
    void Update()
    {
        if (enemyScript.enemyTakeDamage == true)
        {
            Debug.Log("CHANGING COLOR");
            enemyDeadTimer = 20f;
            enemyDeadTimer = Mathf.Clamp(enemyDeadTimer, 0, enemyScript.health);

            // Fade between original enemy color to dead enemy color depending on enemyDeadTimer/enemyScript.health;
            altColor = UnityEngine.Color.Lerp(originalEnemyColor, deadEnemyColor, enemyDeadTimer / enemyScript.health);
            rend.material.color = altColor;
        }
    }
    /*
    void Update()
    {
        if (enemyScript.enemyTakeDamage==true)
        {
            //Alter the color          
            altColor.g += 0.4f;
            //Assign the changed color to the material.
            rend.material.color = altColor;
        }

    } 
    */
}
