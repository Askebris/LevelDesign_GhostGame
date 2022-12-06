using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3f);
    }
}
