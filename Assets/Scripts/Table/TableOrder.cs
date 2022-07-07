using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    [SerializeField] Transform stacks;

    public List<GameObject> customers = new List<GameObject>();
    List<GameObject> coins = new List<GameObject>();

    public int burgerNeeded;
    public int burgerGived;

    public bool isCustomerArrived { get; set; }

    float timer;

    bool canPuttingBurger;

    bool timeToCollectCoin;

    [SerializeField] OrderCanvas canvas;

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
                canvas.ShowCanvas(isCustomerArrived);
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

            if (timeToCollectCoin)
            {
                timeToCollectCoin = false;

                for (int i = 0; i < burgerNeeded * 2; i++)
                    Destroy(coins[i]);

                GameManager.Instance.coin += burgerNeeded * 2;

                coins.Clear();
                customers.Clear();
                table.ClearTable(tableNumber);
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
        canvas.ShowCanvas(isCustomerArrived);

        yield return new WaitForSeconds(5f);

        foreach (GameObject go in customers)
            go.GetComponent<CustomerController>().LeaveRestourant();

        StartCoroutine(CoinRain(burgerNeeded * 2));



        yield return null;
    }

    IEnumerator CoinRain(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject go = Instantiate(table.coinPrefab, new Vector3(transform.position.x, 4f, transform.position.z), table.coinPrefab.transform.rotation, transform.parent.GetChild(3).transform);
            coins.Add(go);
        }
        timeToCollectCoin = true;
        yield return null;
    }

    void SetOrder()
    {
        burgerGived = 0;
        burgerNeeded = table.burgerOrder[tableNumber];
    }
}
