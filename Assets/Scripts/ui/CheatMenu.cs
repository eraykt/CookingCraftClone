using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatMenu : MonoBehaviour
{
    public Button menu, close, level1, level2, level3, addMoney;

    public Image panel;

    public void MenuButton()
    {
        menu.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
    }

    public void CloseButton()
    {
        panel.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void Level(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void AddMoney()
    {
        GameManager.Instance.coin += 100;
    }
   
}
