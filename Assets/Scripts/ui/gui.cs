using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class gui : MonoBehaviour
{
    public TextMeshProUGUI coin;
    private void Update()
    {
        coin.text = GameManager.Instance.coin.ToString();
    }
}
