using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] CustomerPrefabs;
    [SerializeField] Transform SpawnPoint;

    [SerializeField] float min, max;

    [SerializeField] float distance;

    int groupSize;
    int activeGroup;
    float timer;

    bool isFirstCustomerArrived;

    private void Start()
    {
        StartCoroutine(FirstSpawn());
    }

    private void Update()
    {
        if (isFirstCustomerArrived)
            Spawner();
    }

    private void Spawner()
    {
        if (activeGroup < 3)
            timer += Time.deltaTime;

        if (timer > Random.Range(min, max) && activeGroup < 3)
        {
            timer = 0f;
            groupSize = Random.Range(1, 4);

            activeGroup++;

            for (int i = 0; i < groupSize; i++)
            {
                Instantiate(CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)], SpawnPoint.position + new Vector3(0f, 0f, i * distance), new Quaternion(0f,180f,0f,0f), this.transform);
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
