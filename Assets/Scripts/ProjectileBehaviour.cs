using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    float lifeTime = 5f;
    float damage = 5f;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<EnemyBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}
