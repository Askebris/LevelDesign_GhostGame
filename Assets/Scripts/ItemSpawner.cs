using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnItem; 
        
        private bool readyToSpawn = false;
        [SerializeField] private float spawnCD;
        private AudioManager audioManager;
        private GameObject item;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
        {
            Invoke(nameof(ResetSpawn), spawnCD);
        }
    
        void Update()
        {
            if (GameObject.Find(spawnItem.ToString()) != null)
            {
                item = GameObject.Find(spawnItem.ToString()).gameObject;
            }
            
            if (readyToSpawn && item == null)
            {
                spawnDesiredItem();
            }
    
            if (item == null && !IsInvoking())
            {
                Invoke(nameof(ResetSpawn), spawnCD);
            }
        }
    
        private void spawnDesiredItem()
        {
            audioManager.Play("spawnpickup");
            item = Instantiate(spawnItem, transform.position, Quaternion.identity);
            readyToSpawn = false;
        }
    
        private void ResetSpawn()
        {
            readyToSpawn = true;
        }
}
