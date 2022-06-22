using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableScript : MonoBehaviour
{
    bool jobDone;
    bool isCollecting;
    public int tableCoin = 0;
    public int gerekliCoin = 10;
    public GameObject Table, Player;
    private bool isTriggered = false;

    Coroutine putcoin;

    [SerializeField]
    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!jobDone)
            {
                Table.transform.localScale = scaleChange * 2;
                putcoin = StartCoroutine(putCoin());

            }

            isCollecting = true;
        }

    }
    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (!jobDone)
            {
                Table.transform.localScale = scaleChange;
                StopCoroutine(putcoin);
            }

            if (putcoin != null && jobDone)
            {
                StopCoroutine(putcoin);
            }
            isCollecting = false;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (tableCoin == gerekliCoin && !jobDone)
            {
                isCollecting = false;
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                Destroy(this.gameObject.transform.GetChild(0).gameObject);
                jobDone = true;
                if (putcoin != null)
                {
                    StopCoroutine(putCoin());
                }

            }
        }
    }
    IEnumerator putCoin()
    {
        while (true)
        {
            if (isCollecting == true)
            {
                if (tableCoin < gerekliCoin)
                {
                    if (GameManager.Instance.coin > 0)
                    {
                        GameManager.Instance.coin--;
                        tableCoin++;
                    }
                }

            }
            yield return new WaitForSeconds(0.25f);
        }

    }
}
