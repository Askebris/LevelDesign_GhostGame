using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] GameObject Destructablewall;
    [SerializeField] GameObject OpenWall;
    
    public bool WallIsDestroyed = false;
    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("trigger");

        if (collision.gameObject.tag == "Projectile")
        {
            Destructablewall.SetActive(false);
            WallIsDestroyed = true;
            Destroy(gameObject);
            Debug.Log("Wall destroyed");
            if (WallIsDestroyed == true)
            {
                OpenWall.SetActive(true);
                Debug.Log("Wall is open");
            }
            

        }
    }
}
