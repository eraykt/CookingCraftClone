using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] private ObjectPool objectPool = null;

    private void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
           var obj = objectPool.GetPooledObject();

            obj.transform.position =new Vector3 (-3*1-1, 2 , -5);

            yield return new WaitForSeconds(spawnInterval);
            
        }
    }
}
