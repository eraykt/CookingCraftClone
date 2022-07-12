using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeUpgrade : Ground
{
    [SerializeField] GameObject stand;
    [SerializeField] Amele amele;

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
        amele.gameObject.SetActive(true);
        Destroy(stand);
    }
}
