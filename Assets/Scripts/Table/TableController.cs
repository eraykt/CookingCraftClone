using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [Header("Table Transforms")]
    public Transform[] tables;
    public List<Transform> tablesList = new List<Transform>();

    [Header("Avaliable Tables")]
    public bool[] isEmpty;
    public int tableCount;


    [Header("Orders")]
    public int[] burgerOrder = new int[3];

    public GameObject coinPrefab;
    public void ClearTable(int tableNo)
    {
        isEmpty[tableNo] = true;
        tableCount++;
        burgerOrder[tableNo] = 0;
    }
}
