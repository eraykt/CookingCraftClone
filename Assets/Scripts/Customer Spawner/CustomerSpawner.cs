using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] CustomerPrefabs;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] TableController table;


    [SerializeField] float min, max;

    [SerializeField] float distance;

    int groupSize;
    int activeGroup;
    float timer;

    bool isFirstCustomerArrived;

    private void Start()
    {
        //StartCoroutine(FirstSpawn());
    }

    private void Update()
    {
        //if (isFirstCustomerArrived)
        Spawner();
    }

    private void Spawner()
    {
        if (table.tableCount != 0)
            timer += Time.deltaTime;

        if (timer > Random.Range(min, max) && table.tableCount != 0)
        {
            timer = 0f;
            groupSize = Random.Range(1, 4);

            activeGroup++;
            int j;
            for (j = 0; j < 3; j++)
            {
                if (table.isEmpty[j])
                {
                    table.isEmpty[j] = false;
                    table.tableCount--;
                    break;
                }
            }


            for (int i = 0; i < groupSize; i++)
            {
                GameObject go = Instantiate(CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)], SpawnPoint.position + new Vector3(0f, 0f, i * distance), new Quaternion(0f, 180f, 0f, 0f), this.transform);
                go.GetComponent<CustomerController>().SetPos(table.tables[j].GetChild(i).transform);
                table.burgerOrder[j] += go.GetComponent<CustomerController>().BurgerOrder();
            }
        }
    }

    IEnumerator FirstSpawn()
    {
        yield return new WaitForSeconds(5f);
        groupSize = Random.Range(1, 4);
        activeGroup++;
        for (int i = 0; i < groupSize; i++)
        {
            Instantiate(CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)], SpawnPoint.position + new Vector3(0f, 0f, i * distance), new Quaternion(0f, 180f, 0f, 0f), this.transform);
        }
        isFirstCustomerArrived = true;
        yield return null;
    }
}
