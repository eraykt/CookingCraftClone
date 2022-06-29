using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public int index { get; set; }
    public bool isCollecting { get; private set; }

    public float arrivingTime = 5f;
    int generated;


    public Transform truckStack;
    public TruckController truck;

    Coroutine collecting, stacks;

    public bool isTruckArrived, isTruckLeaving;


    private void Start()
    {
        StartCoroutine(Generator());
        Time.timeScale = 5f;
    }


    private void Update()
    {
        if (index < transform.childCount)
        {
            truck.isTruckNeeded = true;
        }
    }



    IEnumerator Generator()
    {
        while (true)
        {
            if (index < transform.childCount && isTruckArrived)
            {
                yield return new WaitForSeconds(GameManager.Instance.generatingSpeed);
                transform.GetChild(index).gameObject.SetActive(true);
                truckStack.GetChild(truckStack.parent.GetComponent<TruckController>().truckIndex--).gameObject.SetActive(false);
                index++; generated++;

                if (truck.truckIndex < 0)
                {
                    isTruckArrived = false; isTruckLeaving = true;
                    truck.PlayLeavingAnimation();
                }
                //if (generated % 9 == 0)
                //{
                //    isTruckArrived = false; isTruckLeaving = true;
                //    truck.PlayLeavingAnimation();
                //    yield return new WaitForSeconds(arrivingTime);
                //}

                yield return null;
            }
            if (index >= transform.childCount && truck.isTruckNeeded)
            {
                if (!isTruckLeaving)
                {
                    truck.PlayLeavingAnimation();
                }
                Debug.Log("a");
                isTruckArrived = false; isTruckLeaving = true;
                truck.isTruckNeeded = false;
                StopCoroutine(truck.TruckArriving());
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
