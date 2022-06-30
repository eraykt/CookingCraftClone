using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject RightDoor, LeftDoor;
    public Animator anim1,anim2;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("asd");
            anim1.Play("DoorAnimation_1");
            anim2.Play("DoorAnimation_2");
        }
    }
}
