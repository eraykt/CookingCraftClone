using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public int index { get; set; }

    public float speed;
    public float CollectingSpeed;

    Coroutine generator, collecting;

    private void Start()
    {
        generator = StartCoroutine(Generator());
    }


    IEnumerator Generator()
    {
        while (index < transform.childCount)
        {
            yield return new WaitForSeconds(speed);
            transform.GetChild(index).gameObject.SetActive(true);
            index++;
            yield return null;
        }
    }

    IEnumerator Collecting()
    {
        while (index > 0)
        {
            yield return new WaitForSeconds(CollectingSpeed);
            index--;
            transform.GetChild(index).gameObject.SetActive(false);
            yield return null;
        }
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        StartCoroutine(Collecting());
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(generator);
            collecting = StartCoroutine(Collecting());
        }


    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.name}");
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(collecting);
            generator = StartCoroutine(Generator());
        }

    }
}
