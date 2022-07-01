using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    int burgerNeeded;
    int burgerGived;


    private void Update()
    {
        if (burgerNeeded == 0 && table.burgerOrder[tableNumber] != 0)
        {
            burgerNeeded = table.burgerOrder[tableNumber];
        }
    }





}
