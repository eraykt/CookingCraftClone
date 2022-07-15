using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : Ground
{
    [SerializeField] Animator animator;

    private void Update()
    {
        if (currentCoin != neededCoin && jobDone)
        {
            // next level with save
            ComplateLevel();
        }

        if (currentCoin == neededCoin && !jobDone)
        {
            jobDone = true;
            ComplateLevel();
        }

        text.text = $"{neededCoin - currentCoin}";

    }

    void ComplateLevel()
    {
        // animasyon
        animator.SetTrigger("LevelUp");
        Destroy(transform.parent.gameObject);
        // sonraki level yüklenecek
    }
}
