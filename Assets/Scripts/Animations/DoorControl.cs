using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

    private Animator anim;

    [SerializeField]private bool Kitchen;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && Kitchen != true)
        {
            anim.Play("GateAnimation_2");
            Kitchen = true;
        }
        if (other.gameObject.CompareTag("Player") && Kitchen == true)
        {  
            anim.Play("GateAnimation");
            Kitchen = false;
        }
    }
    
}
