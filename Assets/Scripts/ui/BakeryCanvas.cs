using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryCanvas : MonoBehaviour
{
    BakeryIn bakery;
    [SerializeField] TMPro.TextMeshProUGUI text, max;
    [SerializeField] Camera cam;
    private void Awake()
    {
        bakery = transform.parent.GetComponentInChildren<BakeryIn>();
    }

    private void Update()
    {
        text.text = $"{bakery.current}/{bakery.max}";
        max.gameObject.SetActive(bakery.current == bakery.max);
    }

}
