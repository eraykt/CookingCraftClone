using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderCanvas : MonoBehaviour
{
    [SerializeField] int currentLevel;


    [SerializeField] TMPro.TextMeshProUGUI bText, hText, pText;




    TableOrder table;
    [SerializeField] Camera cam;


    private void Awake()
    {
        table = GetComponentInParent<TableOrder>();
    }

    private void Update()
    {
        transform.LookAt(cam.transform);

        switch (currentLevel)
        {
            case 1:
                bText.text = table.burgerNeeded - table.burgerGived + "";
                break;

            case 2:
                hText.text = table.hotdogNeeded - table.hotdogGived + "";
                pText.text = table.pizzaNeeded - table.pizzaGived + "";
                break;
        }
    }

    public void ShowCanvas(bool open)
    {
        this.gameObject.SetActive(open);
    }

}
