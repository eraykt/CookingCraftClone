using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : Ground
{
    [SerializeField] int upgradeAmount;

    private void Update()
    {
        if (currentCoin != neededCoin && jobDone)
        {
            // upgrade with save
            MakeUpgrade();

        }

        if (currentCoin == neededCoin && !jobDone)
        {
            jobDone = true;
            MakeUpgrade();
        }

        text.text = $"{currentCoin}/{neededCoin}";
    }


    void MakeUpgrade()
    {
        GameManager.Instance.PlayerStackLimit = upgradeAmount;
        Destroy(transform.parent.gameObject);
    }
}
