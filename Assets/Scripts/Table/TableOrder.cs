using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    [SerializeField] Transform stacks;

    public int burgerNeeded;
    public int burgerGived;

    float timer;

    bool canPuttingBurger;

    private void Awake()
    {
        table = GetComponentInParent<TableController>();
    }

    private void Update()
    {
        if (burgerNeeded == 0 && table.burgerOrder[tableNumber] != 0)
        {
            burgerNeeded = table.burgerOrder[tableNumber];
        }

        if (canPuttingBurger && burgerGived < burgerNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutBurgerToTable();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (burgerGived < burgerNeeded)
            {
                if (GameManager.Instance.PlayerStack > 0 && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Burger"))
                {
                    canPuttingBurger = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canPuttingBurger)
        {
            canPuttingBurger = false;
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

}
