using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ground : MonoBehaviour
{
    public int currentCoin;
    public int neededCoin;
    public bool jobDone;
    public bool isCollecting;
    public GameObject area;
    public TMPro.TextMeshPro text;

    public float size = 2;

    public Coroutine putcoin;

    AudioSource coinSound;

    float speed = 0;

    private void Awake()
    {
        coinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!jobDone)
            {
                area.transform.localScale = area.transform.localScale * size;
                text.transform.localScale = text.transform.localScale * size;
                putcoin = StartCoroutine(putCoin());

            }
            isCollecting = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentCoin == neededCoin && !jobDone)
            {
                isCollecting = false;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            if (!jobDone)
            {
                area.transform.localScale = area.transform.localScale / size;
                text.transform.localScale = text.transform.localScale / size;
                StopCoroutine(putcoin);
                speed = 0;
            }

            if (putcoin != null && jobDone)
            {
                StopCoroutine(putcoin); putcoin = null;
            }
            isCollecting = false;

        }
    }

    IEnumerator putCoin()
    {
        while (true)
        {
            if (isCollecting == true)
            {
                if (currentCoin < neededCoin)
                {
                    if (GameManager.Instance.coin > 0)
                    {
                        if (!coinSound.isPlaying)
                            coinSound.Play();


                        GameManager.Instance.coin--;
                        currentCoin++;
                        speed += 0.002f;
                    }
                }
            }
            yield return new WaitForSeconds(0.15f - speed);
        }

    }
}
