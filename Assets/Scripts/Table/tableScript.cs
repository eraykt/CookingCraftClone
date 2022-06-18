using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableScript : MonoBehaviour
{
    public GameObject Table,Player;
    private bool isTriggered = false;
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
        if (col.gameObject.tag=="Player")
        {
            Table.transform.localScale = scaleChange * 2;
        }
        
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Table.transform.localScale = scaleChange;
        }
    }
}
