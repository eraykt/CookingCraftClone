using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    int i;

    public float speed;

    private void Start()
    {
        StartCoroutine(Generator());
    }


    IEnumerator Generator()
    {
        while (i < transform.childCount)
        {
            yield return new WaitForSeconds(speed);
            transform.GetChild(i).gameObject.SetActive(true);
            i++;
            yield return null;
        }

    }
}
