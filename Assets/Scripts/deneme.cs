using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class deneme : MonoBehaviour
{
    [SerializeField] Transform pos;
    public NavMeshAgent agent;
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(pos.position);
    }
}
