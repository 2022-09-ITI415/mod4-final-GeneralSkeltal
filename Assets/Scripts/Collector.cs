using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIController>())
        {
            Destroy(other.gameObject);
            inventory.animalCount++;
        }
    }
}
