using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            Destroy(other.gameObject);
        }
    }
}
