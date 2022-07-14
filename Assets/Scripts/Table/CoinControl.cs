using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    public bool isHittedToPlayer { get; private set; }
    public void MoveToPlayer()
    {
        Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 0.5f);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isHittedToPlayer = true;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHittedToPlayer = true;
        }
    }
}
