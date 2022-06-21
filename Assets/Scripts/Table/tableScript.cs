using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableScript : MonoBehaviour
{
    bool jobDone;
    bool isCollecting;
    public int tableCoin = 0;
    public int gerekliCoin = 10;
    public GameObject Table,Player;
    private bool isTriggered = false;
    [SerializeField]
    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(1, 0, 1);
        StartCoroutine(putCoin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            if (!jobDone)
            {
                Table.transform.localScale = scaleChange * 2;
            }
            
            isCollecting=true;
        }
        
    }
    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (!jobDone)
            {
                Table.transform.localScale = scaleChange;
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
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                Destroy(this.gameObject.transform.GetChild(0).gameObject);
                jobDone = true;
                StopCoroutine(putCoin());

            }
        }
    }
    IEnumerator putCoin()
    {
        while (true)
        {
            Debug.Log("0");
            if (isCollecting == true)
            {
                Debug.Log("1");
                if (tableCoin < gerekliCoin)
                {
                    Debug.Log("2");
                    if (GameManager.Instance.coin >0)
                    {
                        Debug.Log("3");
                        GameManager.Instance.coin--;
                        tableCoin++;
                    }
                }

            }
            yield return new WaitForSeconds(0.25f);
        }

    }
}
