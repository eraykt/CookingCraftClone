using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryIn : MonoBehaviour
{
    public int current;
    [SerializeField] int max;
    [SerializeField] BakeryOut bakeryOut;
    public GameObject hmadde,hamburger;
    Animator anim,anim2;
    bool burgerAnim = false;




    float timer;


    bool isCollecting;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim2 = GetComponent<Animator>();
        bakeryOut = transform.parent.GetComponentInChildren<BakeryOut>();
        timer = GameManager.Instance.puttingSpeed;
        hmadde.SetActive(false);
        hamburger.SetActive(false);
        burgerAnim = false;
    }


    private void Update()
    {
        if (isCollecting && current < max && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            AddObjToBakery();
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (GameManager.Instance.PlayerStack > 0 && current < max)
    //    {
    //        isCollecting = true;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.PlayerStack > 0 && current < max)
        {
            isCollecting = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player" && isCollecting == true)
        {
            hmadde.SetActive(true);            
            burgerAnim = true;

            if (burgerAnim == true)
            {
                hamburger.SetActive(true);
            }         
        }

    }
    
}
