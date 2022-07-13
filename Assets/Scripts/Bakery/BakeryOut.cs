using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryOut : MonoBehaviour
{
    public int cookedIndex;
    [SerializeField] int max;
    [SerializeField] Transform mealTransform;
    public Animator Anim;
    BakeryIn bakeryIn;

    [SerializeField] int stackIndex;

    public float generatingSpeed = 3f;
    public bool CanCook = true;

    bool player;

    public int meal;

    Coroutine collecting, stacks;
    Coroutine collectingW, stacksW;

    [SerializeField] Waitress waitress;

    private void Start()
    {
        bakeryIn = transform.parent.GetComponentInChildren<BakeryIn>();
    }

    private void Update()
    {
        if (meal > 0)
        {
            if (CanCook && cookedIndex != 27)
            {
                CanCook = false;
                StartCoroutine(Generator());
            }

        }

        if (!CanCook && cookedIndex == 27)
        {
            StopCoroutine(Generator());
            CanCook = true;
        }
    }

    IEnumerator Generator()
    {
        if (cookedIndex != 27)
        {
            Anim.Play("bakery");
            yield return new WaitForSeconds(generatingSpeed);

            bakeryIn.current--;
            mealTransform.transform.GetChild(cookedIndex).gameObject.SetActive(true);
            cookedIndex++;
            meal--;
            CanCook = true;
        }
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
                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(stackIndex));
            }
        }

        if (other.CompareTag("Waitress"))
        {
            if (waitress.index < waitress.max && cookedIndex > 0)
            {
                collectingW = StartCoroutine(CollectingW());
                stacksW = StartCoroutine(waitress.AddStack(stackIndex));
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit;
            if (collecting == null)
            {
                if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit && cookedIndex > 0)
                {
                    collecting = StartCoroutine(Collecting());
                    stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(stackIndex));

                }
            }

            if (collecting != null)
            {
                if (GameManager.Instance.PlayerStack == GameManager.Instance.PlayerStackLimit || cookedIndex == 0)
                {
                    StopCoroutine(collecting); collecting = null;
                    StopCoroutine(stacks);
                    player = false;
                }
            }

        }

        if (other.CompareTag("Waitress"))
        {
            if (!player)
            {
                if (collectingW == null)
                {
                    if (waitress.index < waitress.max && cookedIndex > 0)
                    {
                        collectingW = StartCoroutine(CollectingW());
                        stacksW = StartCoroutine(waitress.AddStack(stackIndex));
                    }
                }

                if (collectingW != null)
                {
                    if (waitress.index == waitress.max || cookedIndex == 0)
                    {
                        StopCoroutine(collectingW); collectingW = null;
                        StopCoroutine(stacksW);
                    }
                }

            }
        }

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collecting != null)
            {
                StopCoroutine(collecting); collecting = null;
                StopCoroutine(stacks);
            }
            player = false;
        }

        if (other.CompareTag("Waitress"))
        {
            if (collectingW != null)
            {
                StopCoroutine(collectingW); collectingW = null;
                StopCoroutine(stacksW);
            }
        }
    }


    IEnumerator CollectingW()
    {
        while (cookedIndex > 0)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);

            cookedIndex--;
            mealTransform.transform.GetChild(cookedIndex).gameObject.SetActive(false);

            if (waitress.index == waitress.max - 1)
                break;

            yield return null;
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
