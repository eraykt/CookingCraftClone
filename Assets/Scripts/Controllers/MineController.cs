using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public int index { get; set; }
    public bool isCollecting { get; private set; }

    public float generatingSpeed = 0.5f;
    public float collectingSpeed = 0.6f;
    public float arrivingTime = 5f;
    int generated;

    Coroutine collecting, stacks;

    [SerializeField] PlayerStacks pstack;

    private void Start()
    {
        StartCoroutine(Generator());
    }


    IEnumerator Generator()
    {
        while (index < transform.childCount)
        {
            yield return new WaitForSeconds(generatingSpeed);
            transform.GetChild(index).gameObject.SetActive(true);
            index++; generated++;

            if (generated % 9 == 0)
                yield return new WaitForSeconds(arrivingTime);

            yield return null;
        }
    }

    IEnumerator Collecting()
    {
        isCollecting = true;
        while (index > 0)
        {
            yield return new WaitForSeconds(collectingSpeed);
            index--;
            transform.GetChild(index).gameObject.SetActive(false);
            if (GameManager.Instance.PlayerStack == GameManager.Instance.PlayerStackLimit - 1)
                break;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
            {
                collecting = StartCoroutine(Collecting());
                stacks = StartCoroutine(pstack.AddStack());
            }

            else
            {
                StopCoroutine(collecting);
                StopCoroutine(stacks);
                isCollecting = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(collecting);
            StopCoroutine(stacks);
            isCollecting = false;
        }
    }
}
