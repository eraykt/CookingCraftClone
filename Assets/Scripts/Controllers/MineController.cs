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
    Coroutine collectingE, stacksE;

    public bool isTruckArrived, isTruckLeaving;


    private void Start()
    {
        StartCoroutine(Generator());
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
            if (index >= transform.childCount && isTruckArrived)
            {
                if (!isTruckLeaving)
                {
                    truck.PlayLeavingAnimation();
                }
                isTruckArrived = false; isTruckLeaving = true;
                truck.isTruckNeeded = false;
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
            if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit && index > 0)
            {
                collecting = StartCoroutine(Collecting());
                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(0));
            }
        }

        if (other.gameObject.CompareTag("Amele"))
        {
            if (Amele.instance.holding < Amele.instance.limit && index > 0)
            {
                collectingE = StartCoroutine(CollectingE());
                stacksE = StartCoroutine(Amele.instance.AddStack(0));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (collecting == null)
            {
                if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit && index > 0)
                {
                    collecting = StartCoroutine(Collecting());
                    stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(0));
                }
            }

            if (collecting != null)
            {
                if (GameManager.Instance.PlayerStack == GameManager.Instance.PlayerStackLimit || index == 0)
                {
                    StopCoroutine(collecting); collecting = null;
                    StopCoroutine(stacks);
                    isCollecting = false;
                }
            }
        }

        if (other.CompareTag("Amele"))
        {
            if (collectingE == null)
            {
                if (Amele.instance.holding < Amele.instance.limit && index > 0)
                {
                    collectingE = StartCoroutine(CollectingE());
                    stacksE = StartCoroutine(Amele.instance.AddStack(0));
                }
            }
            if (collectingE != null)
            {
                if (Amele.instance.holding == Amele.instance.limit || index == 0)
                {
                    StopCoroutine(collectingE); collectingE = null;
                    StopCoroutine(stacksE);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (collecting != null)
            {
                StopCoroutine(collecting);
                StopCoroutine(stacks);
                isCollecting = false;
            }
        }

        if (other.CompareTag("Amele"))
        {
            if (collectingE != null)
            {
                StopCoroutine(collectingE);
                StopCoroutine(stacksE);
            }
        }
    }


    IEnumerator CollectingE()
    {
        while (index > 0)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);
            index--;
            transform.GetChild(index).gameObject.SetActive(false);
            if (Amele.instance.holding == Amele.instance.limit - 1)
                break;

            yield return null;
        }
    }

}
