using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryOut : MonoBehaviour
{
    //public GameObject hammadde;
    public int cookedIndex;
    //Animator animator;
    [SerializeField] int max;
    [SerializeField] Transform mealTransform;
    public Animator Anim;
    BakeryIn bakeryIn;

    public float generatingSpeed = 3f;
    public bool CanCook = true;

    public int meal;
    Coroutine collecting, stacks;

    private void Start()
    {
        // gameObject.SetActive(false);
        bakeryIn = transform.parent.GetComponentInChildren<BakeryIn>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (meal > 0)
        {
            //gameObject.SetActive(true);
            if (CanCook)
            {
                CanCook = false;
                StartCoroutine(Generator());

                // animasyon
            }
        }
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    IEnumerator Generator()
    {
        //Anim.SetTrigger("Bakery");
        Anim.Play("bakery");
        yield return new WaitForSeconds(generatingSpeed);

        //Anim.Play("bakery");

        bakeryIn.current--;
        mealTransform.transform.GetChild(cookedIndex).gameObject.SetActive(true);
        cookedIndex++;
        meal--;
        CanCook = true;

        yield return null;
    }


    IEnumerator Collecting()
    {
        while (cookedIndex > 0)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);

            cookedIndex--;
            mealTransform.transform.GetChild(cookedIndex).gameObject.SetActive(false);

            if (GameManager.Instance.PlayerStack == GameManager.Instance.PlayerStackLimit - 1)
                break;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit && cookedIndex > 0)
            {
                collecting = StartCoroutine(Collecting());
                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collecting != null)
            {
                if (GameManager.Instance.PlayerStack == GameManager.Instance.PlayerStackLimit || cookedIndex == 0)
                {
                    StopCoroutine(collecting);
                    StopCoroutine(stacks);
                }
            }

            else
            {
                if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit && cookedIndex > 0)
                {
                    collecting = StartCoroutine(Collecting());
                    stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
                }
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (collecting != null && stacks != null)
            {
                StopCoroutine(collecting);
                StopCoroutine(stacks);
            }

        }
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
    //        {
    //            if (canCollect)
    //            {
    //                canCollect = false;
    //                collecting = StartCoroutine(Collecting());
    //                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
    //            }
    //        }

    //        if (collecting != null && cookedIndex == 0)
    //        {
    //            StopCoroutine(collecting);
    //            StopCoroutine(stacks);
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
    //        {
    //            if (cookedIndex < 0)
    //            {
    //                canCollect = true;
    //            }
    //        }
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
    //        {
    //            if (cookedIndex > 0 && mealTransform.transform.GetChild(0).gameObject.activeSelf)
    //            {
    //                collecting = StartCoroutine(Collecting());
    //                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
    //            }
    //        }

    //        else if (collecting != null && stacks != null)
    //        {
    //            StopCoroutine(collecting);
    //            StopCoroutine(stacks);
    //        }
    //    }
    //}


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (collecting == null && stacks == null)
    //        {
    //            if (cookedIndex > 0 && (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit))
    //            {
    //                if (canCollect)
    //                {
    //                    Debug.Log("asd");
    //                    canCollect = false;
    //                    collecting = StartCoroutine(Collecting());
    //                    stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
    //                }

    //                if (cookedIndex == 0 && collecting != null)
    //                {
    //                    StopCoroutine(collecting);
    //                    StopCoroutine(stacks);
    //                }
    //            }
    //        }
    //    }
    //}


}
