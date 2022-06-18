using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public int index { get; set; }
    public bool isCollecting { get; private set; }

    public float arrivingTime = 5f;
    int generated;

    Coroutine collecting, stacks;


    private void Start()
    {
        StartCoroutine(Generator());
    }


    IEnumerator Generator()
    {
        while (true)
        {
            if (index < transform.childCount)
            {
                yield return new WaitForSeconds(GameManager.Instance.generatingSpeed);
                transform.GetChild(index).gameObject.SetActive(true);
                index++; generated++;

                if (generated % 9 == 0)
                    yield return new WaitForSeconds(arrivingTime);

                yield return null;
            }
            yield return null;
        }
    }

    IEnumerator Collecting()
    {
        isCollecting = true;
        while (index > 0)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);
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
                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(0));
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
