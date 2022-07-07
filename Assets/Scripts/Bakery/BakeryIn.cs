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


    bool isCollecting;
    private void Start()
    {
        bakeryOut = transform.parent.GetComponentInChildren<BakeryOut>();
        timer = GameManager.Instance.puttingSpeed;

    }


    private void Update()
    {
        if (isCollecting && current < max && GameManager.Instance.PlayerStack > 0 )
        {
            timer -= Time.deltaTime;
            AddObjToBakery();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.PlayerStack > 0 && current < max && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Hammadde"))
        {
            isCollecting = true;
        }

        if (!stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Hammadde"))
        {
            isCollecting = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCollecting)
        {
            isCollecting = false;
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
}
