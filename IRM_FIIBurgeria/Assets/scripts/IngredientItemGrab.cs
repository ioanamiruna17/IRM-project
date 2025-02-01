using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    public GameObject itemPrefab; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) 
        {
            SpawnItem(other.transform.position);
        }
    }

    private void SpawnItem(Vector3 spawnPosition)
    {
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }
}
