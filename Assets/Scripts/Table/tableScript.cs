using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableScript : MonoBehaviour
{
    public bool jobDone;
    bool isCollecting;
    public int tableCoin = 0;
    public int gerekliCoin = 10;
    public GameObject Table;
    public TMPro.TextMeshPro text;
    public Collider col;

    static int i = 0;

    public TableController TableController;

    Coroutine putcoin;

    [SerializeField]
    private Vector3 scaleChange;

    float coinSpeed;

    void Start()
    {
        scaleChange = new Vector3(1, 0, 1);
    }

    void Update()
    {
        if (gerekliCoin != tableCoin && jobDone)
        {
            BuildTable();
        }

        text.text = $"{gerekliCoin - tableCoin}";
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!jobDone)
            {
                Table.transform.localScale = scaleChange * 2;
                text.transform.localScale = text.transform.localScale * 2;
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
                text.transform.localScale = text.transform.localScale / 2;
                StopCoroutine(putcoin);
                coinSpeed = 0;
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
                BuildTable();

            }
        }
    }

    private void BuildTable()
    {
        this.gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);

        col.enabled = true;

        TableController.tableCount++;
        TableController.isEmpty[i++] = true;

        if (i < 3)
            TableController.tablesList[i].gameObject.SetActive(true);




        jobDone = true;

        if (putcoin != null)
        {
            StopCoroutine(putCoin());
        }

        Destroy(Table.gameObject);
        Destroy(text.gameObject);
        Destroy(this.gameObject);
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
                        coinSpeed += 0.002f;
                    }
                }

            }
            yield return new WaitForSeconds(0.15f - coinSpeed);
        }

    }
}
