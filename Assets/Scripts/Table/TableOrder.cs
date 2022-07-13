using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    [SerializeField] Transform stacks;
    [SerializeField] Transform stacksW;

    public List<GameObject> customers = new List<GameObject>();
    List<GameObject> coins = new List<GameObject>();

    public int burgerNeeded;
    public int burgerGived;

    public int hotdogNeeded;
    public int hotdogGived;

    public int pizzaNeeded;
    public int pizzaGived;

    bool jobDone;

    public bool isCustomerArrived { get; set; }

    float timer, timerW;

    bool canPuttingBurger;
    bool canPuttingBurgerW;
    bool canPuttingHotdog;
    bool canPuttingHotdogW;
    bool canPuttingPizza;
    bool canPuttingPizzaW;

    bool timeToCollectCoin;

    [SerializeField] OrderCanvas canvas;

    [SerializeField] Waitress waitress;

    private void Awake()
    {
        table = GetComponentInParent<TableController>();
    }

    private void Start()
    {
        timer = GameManager.Instance.puttingSpeed;
        timerW = GameManager.Instance.puttingSpeed;
    }

    private void Update()
    {
        if (canPuttingBurger && burgerGived < burgerNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutBurgerToTable();
        }

        if (canPuttingBurgerW && burgerGived < burgerNeeded && waitress.index > 0)
        {
            timerW -= Time.deltaTime;
            PutBurgerToTableW();
        }


        if (canPuttingHotdog && hotdogGived < hotdogNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutHotdogToTable();
        }

        if (canPuttingHotdogW && hotdogGived < hotdogNeeded && waitress.index > 0)
        {
            timerW -= Time.deltaTime;
            PutHotdogToTableW();
        }

        if (canPuttingPizza && pizzaGived < pizzaNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutPizzaToTable();
        }

        if (canPuttingPizzaW && pizzaGived < pizzaNeeded && waitress.index > 0)
        {
            timerW -= Time.deltaTime;
            PutPizzaToTableW();
        }

        if (burgerGived == burgerNeeded && pizzaNeeded == pizzaGived && hotdogNeeded == hotdogGived && isCustomerArrived)
        {
            jobDone = true;
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

            if (hotdogGived < hotdogNeeded)
            {
                canPuttingHotdog = GameManager.Instance.PlayerStack > 0 && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Sosisli") && isCustomerArrived;
            }

            if (pizzaGived < pizzaNeeded)
            {
                canPuttingPizza = GameManager.Instance.PlayerStack > 0 && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Pizza") && isCustomerArrived;
            }

            if (timeToCollectCoin)
            {
                timeToCollectCoin = false;

                for (int i = 0; i < (burgerNeeded * 2) + hotdogNeeded + pizzaNeeded; i++)
                    Destroy(coins[i]);

                GameManager.Instance.coin += (burgerNeeded * 2) + hotdogNeeded + pizzaNeeded;

                coins.Clear();
                customers.Clear();
                table.ClearTable(tableNumber);
            }
        }


        if (other.CompareTag("Waitress"))
        {
            if (burgerGived < burgerNeeded)
            {
                canPuttingBurgerW = waitress.index > 0 && isCustomerArrived;
            }

            if (hotdogGived < hotdogNeeded)
            {
                canPuttingHotdogW = waitress.index > 0 && stacksW.GetChild(waitress.index).gameObject.CompareTag("Sosisli") && isCustomerArrived;
            }

            if (pizzaGived < pizzaNeeded)
            {
                canPuttingPizzaW = waitress.index > 0 && stacksW.GetChild(waitress.index).gameObject.CompareTag("Pizza") && isCustomerArrived;
            }
        }
        #region Siparisler tamamlandiginda

        if (jobDone /*&& burgerGived != 0*/)
        {
            jobDone = false;
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
                canPuttingHotdog = false;
                canPuttingPizza = false;
            }
        }

        if (other.CompareTag("Waitress"))
        {
            if (canPuttingBurgerW)
            {
                canPuttingBurgerW = false;
                canPuttingHotdogW = false;
                canPuttingPizzaW = false;
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

    void PutHotdogToTable()
    {
        if (timer < 0f)
        {
            hotdogGived++;
            PlayerStacks.StackInstance.RemoveStack();
            timer = GameManager.Instance.puttingSpeed;
        }

        if (hotdogGived == hotdogNeeded)
        {
            canPuttingHotdog = false;
        }
    }

    void PutPizzaToTable()
    {
        if (timer < 0f)
        {
            pizzaGived++;
            PlayerStacks.StackInstance.RemoveStack();
            timer = GameManager.Instance.puttingSpeed;
        }

        if (pizzaGived == pizzaNeeded)
        {
            canPuttingPizza = false;
        }
    }



    void PutBurgerToTableW()
    {
        if (timerW < 0f)
        {
            burgerGived++;
            waitress.RemoveStack();
            timerW = GameManager.Instance.puttingSpeed;
        }

        if (burgerGived == burgerNeeded)
        {
            canPuttingBurgerW = false;
        }
    }

    void PutHotdogToTableW()
    {
        if (timerW < 0f)
        {
            hotdogGived++;
            waitress.RemoveStack();
            timerW = GameManager.Instance.puttingSpeed;
        }

        if (hotdogGived == hotdogNeeded)
        {
            canPuttingHotdogW = false;
        }
    }

    void PutPizzaToTableW()
    {
        if (timerW < 0f)
        {
            pizzaGived++;
            waitress.RemoveStack();
            timerW = GameManager.Instance.puttingSpeed;
        }

        if (pizzaGived == pizzaNeeded)
        {
            canPuttingPizzaW = false;
        }
    }

    IEnumerator Completed()
    {
        burgerGived = 0;
        hotdogGived = 0;
        pizzaGived = 0;
        isCustomerArrived = false;
        canvas.ShowCanvas(isCustomerArrived);

        yield return new WaitForSeconds(5f);

        foreach (GameObject go in customers)
            go.GetComponent<CustomerController>().LeaveRestourant();

        StartCoroutine(CoinRain((burgerNeeded * 2) + hotdogNeeded + pizzaNeeded));



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
        pizzaGived = 0;
        hotdogGived = 0;
        burgerNeeded = table.burgerOrder[tableNumber];
        pizzaNeeded = table.pizzaOrder[tableNumber];
        hotdogNeeded = table.hotdogOrder[tableNumber];
    }
}
