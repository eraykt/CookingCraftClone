using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
<<<<<<< HEAD
    
    Animator anim;
=======
    //public Collider Kitchen, Saloon;
    //public GameObject RightDoor, LeftDoor;
    public Animator anim;
>>>>>>> 340f3d93cca95ad8656c77d3255b5beacad9154f
    [SerializeField]private bool Kitchen;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && Kitchen != true)
        {
            Debug.Log("asd");
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
