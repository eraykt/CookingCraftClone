using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class deneme : MonoBehaviour
{
    [SerializeField] Transform pos;
    public NavMeshAgent agent;

    Rigidbody rig;

    bool b = true;
    bool iswalking = true;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        GetComponent<Animator>().SetBool("walk", iswalking);

        if (b)
        {
            agent.SetDestination(pos.position);
            b = false;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.LookAt(pos.parent.transform);
        }

        if (agent.velocity == Vector3.zero)
        {
            iswalking = false;
        }

        else
        {
            iswalking = true;
        }

        //Debug.Log(agent.velocity);
    }


}
