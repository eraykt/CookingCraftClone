using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    [SerializeField] Transform stacks;

    public List<GameObject> customers = new List<GameObject>();

    public int burgerNeeded;
    public int burgerGived;

    bool isCustomerArrived;

    float timer;

    bool canPuttingBurger;

    private void Awake()
    {
        table = GetComponentInParent<TableController>();
    }

    private void Update()
    {
        if (canPuttingBurger && burgerGived < burgerNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutBurgerToTable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            if (other.GetComponent<CustomerController>().settedTable == tableNumber)
            {
                isCustomerArrived = true;
                SetOrder();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (burgerGived < burgerNeeded)
            {
                canPuttingBurger = GameManager.Instance.PlayerStack > 0 && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Burger") && isCustomerArrived;
            }
        }

        #region Siparisler tamamlandiginda

        if (burgerNeeded == burgerGived && burgerGived != 0)
        {
            StartCoroutine(Completed());
        }
        #endregion
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canPuttingBurger)
            {
                canPuttingBurger = false;
            }
        }
    }


    void PutBurgerToTable()
    {
        if (timer < 0f)
        {
            burgerGived++;
            PlayerStacks.StackInstance.RemoveStack();
            timer = GameManager.Instance.puttingSpeed;
        }

        if (burgerGived == burgerNeeded)
        {
            canPuttingBurger = false;
        }
    }

    IEnumerator Completed()
    {
        burgerGived = 0;
        isCustomerArrived = false;

        yield return new WaitForSeconds(5f);

        foreach (GameObject go in customers)
            go.GetComponent<CustomerController>().LeaveRestourant();

        customers.Clear();
        table.ClearTable(tableNumber);

        yield return null;
    }

    void SetOrder()
    {
        burgerGived = 0;
        burgerNeeded = table.burgerOrder[tableNumber];
    }
}
