using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryIn : MonoBehaviour
{
    public int current;
    public int max = 4;
    [SerializeField] BakeryOut bakeryOut;

    [SerializeField] Transform stacks;


    float timer;
    float timerE;


    bool isCollecting;
    bool isCollectingEmployee;
    private void Start()
    {
        bakeryOut = transform.parent.GetComponentInChildren<BakeryOut>();
        timer = GameManager.Instance.puttingSpeed;
        timerE = GameManager.Instance.puttingSpeed;


    }


    private void Update()
    {
        if (isCollecting && current < max && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            AddObjToBakery();
        }


        if (isCollectingEmployee && current < max && Amele.instance.holding > 0)
        {
            timerE -= Time.deltaTime;
            AddObjToBakeryEmployee();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.PlayerStack > 0 && current < max && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Hammadde"))
        {
            isCollecting = true;
        }

        if (!stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Hammadde"))
        {
            isCollecting = false;
        }




        if (other.CompareTag("Amele") && Amele.instance.holding > 0 && current < max && Amele.instance.transform.GetChild(2).GetChild(Amele.instance.holding).gameObject.CompareTag("Hammadde"))
        {
            isCollectingEmployee = true;
        }

        if (!Amele.instance.transform.GetChild(2).GetChild(Amele.instance.holding).gameObject.CompareTag("Hammadde"))
        {
            isCollectingEmployee = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isCollecting)
        {
            isCollecting = false;
        }

        if (other.CompareTag("Amele") && isCollectingEmployee)
        {
            isCollectingEmployee = false;
        }
    }

    private void AddObjToBakery()
    {
        if (timer < 0f)
        {
            current++;
            PlayerStacks.StackInstance.RemoveStack();
            bakeryOut.meal++;
            timer = GameManager.Instance.puttingSpeed;
        }

        if (current == max)
        {
            isCollecting = false;
        }
    }

    private void AddObjToBakeryEmployee()
    {
        if (timerE < 0f)
        {
            current++;
            Amele.instance.RemoveStack();
            bakeryOut.meal++;
            timerE = GameManager.Instance.puttingSpeed;
        }
    }
}
