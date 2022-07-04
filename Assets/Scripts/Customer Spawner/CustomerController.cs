using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    Transform TablePos;
    NavMeshAgent agent;

    bool isWalking;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("walk", isWalking);

        if (agent.velocity == Vector3.zero)
            isWalking = false;

        else
            isWalking = true;


        //if (isWalking)
        //{
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.LookAt(TablePos.parent.transform);
        }
        //}
    }

    public void WalkToTable(Transform pos)
    {
        TablePos = pos;
        agent.SetDestination(pos.position);
        isWalking = true;
    }

    public void LeaveRestourant()
    {
        agent.SetDestination(new Vector3(-3f, 0f, 8.5f));
        isWalking = true;
    }

    public int BurgerOrder()
    {
        return Random.Range(2, 5);
    }
}
