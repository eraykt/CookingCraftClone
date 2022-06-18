using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int coin;
    bool isBought = false;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isBought = true;
            coin = 10;
            if (isBought == true && coin == 10)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
    }
}
