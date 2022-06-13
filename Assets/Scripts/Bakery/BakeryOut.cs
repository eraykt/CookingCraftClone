using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryOut : MonoBehaviour
{
    int cookedIndex;

    [SerializeField] int max;
    [SerializeField] Transform mealTransform;

    BakeryIn bakeryIn;

    public float generatingSpeed = 3f;
    public bool CanCook = true;
    
    public int meal;

    Coroutine collecting, stacks;

    private void Start()
    {
        bakeryIn = transform.parent.GetComponentInChildren<BakeryIn>();
    }

    private void Update()
    {
        if (meal > 0)
        {
            if (CanCook)
            {
                CanCook = false;
                StartCoroutine(Generator());
            }
        }
    }

    IEnumerator Generator()
    {
        yield return new WaitForSeconds(generatingSpeed);
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
            {
                collecting = StartCoroutine(Collecting());
                stacks = StartCoroutine(PlayerStacks.StackInstance.AddStack(1));
            }

            else
            {
                StopCoroutine(collecting);
                StopCoroutine(stacks);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(collecting);
            StopCoroutine(stacks);
        }
    }


}
