using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryUpgrade : Ground
{
    [SerializeField] int upgradeAmount;

    [SerializeField] BakeryIn bakery;
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

        text.text = $"{neededCoin - currentCoin}";
    }


    void MakeUpgrade()
    {
        bakery.max = upgradeAmount;
        Destroy(transform.parent.gameObject);
    }
}
