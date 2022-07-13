using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : Ground
{
    [SerializeField] int upgradeAmount;

    [SerializeField] int currentLevel;

    [SerializeField] PlayerMovement player;

    private void Update()
    {
        if (currentCoin != neededCoin && jobDone)
        {
            // upgrade with save
            MakeUpgradeLevel1();

        }

        if (currentCoin == neededCoin && !jobDone)
        {
            switch (currentLevel)
            {
                case 1:
                    jobDone = true;
                    MakeUpgradeLevel1();
                    break;

                case 2:
                    MakeUpgradeLevel2First();
                    break;

                case 3:
                    MakeUpgradeLevel2Second();
                    break;
            }
        }

        text.text = $"{currentCoin}/{neededCoin}";
    }


    void MakeUpgradeLevel1()
    {
        GameManager.Instance.PlayerStackLimit = upgradeAmount;
        Destroy(transform.parent.gameObject);
    }

    void MakeUpgradeLevel2First()
    {
        GameManager.Instance.PlayerStackLimit = upgradeAmount;
        currentCoin = 0;
        neededCoin = 35;
        currentLevel++;
        jobDone = false;
    }

    void MakeUpgradeLevel2Second()
    {
        player.maxSpeed = 100f;
        Destroy(transform.parent.gameObject);
    }
}
