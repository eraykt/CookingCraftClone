using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitressUpgrade : Ground
{
    [SerializeField] GameObject stand;
    [SerializeField] Waitress waitress;

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
        waitress.gameObject.SetActive(true);
        Destroy(stand);
    }



}
