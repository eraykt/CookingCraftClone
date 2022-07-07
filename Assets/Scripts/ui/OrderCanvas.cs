using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCanvas : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    TableOrder table;
    [SerializeField] Camera cam;

    private void Awake()
    {
        table = GetComponentInParent<TableOrder>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        transform.LookAt(cam.transform);
        text.text = table.burgerNeeded - table.burgerGived + "";
    }

    public void ShowCanvas(bool open)
    {
        this.gameObject.SetActive(open);
    }

}
