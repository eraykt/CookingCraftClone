using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waitress : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] BakeryOut bakery;

    [SerializeField] TableOrder[] tables;
    [SerializeField] Transform[] deliver;

    [SerializeField] Transform objTransform;

    Vector3 sPoint;

    float distance = 0.3f;

    bool isWalking, isHolding;


    public int index, max = 4;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        sPoint = transform.position;
    }

    private void Update()
    {
        if (index == 0 && bakery.cookedIndex > 0)
        {
            agent.SetDestination(bakery.transform.position);

        }

        if (LocateTable() != -1)
        {
            if (index == max || tables[LocateTable()].burgerNeeded - tables[LocateTable()].burgerGived == index)
            {
                agent.SetDestination(deliver[LocateTable()].position);
            }
        }

        if (LocateTable() == -1)
        {
            if (index < max)
                agent.SetDestination(bakery.transform.position);
        }

        isWalking = agent.velocity != Vector3.zero;
        isHolding = index > 0;

        GetComponent<Animator>().SetBool("walk", isWalking);
        GetComponent<Animator>().SetBool("isHolding", isHolding);
    }


    public IEnumerator AddStack(int stackIndex)
    {
        while (index < max)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);
            GameObject go = Instantiate(PlayerStacks.StackInstance.stackPrefabs[stackIndex], new Vector3(objTransform.transform.position.x, objTransform.transform.position.y, objTransform.transform.position.z), PlayerStacks.StackInstance.stackPrefabs[stackIndex].transform.rotation, objTransform.parent.transform);
            go.transform.rotation = objTransform.transform.rotation;
            objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y + distance, objTransform.transform.position.z);
            index++;
            yield return null;
        }
    }

    public void RemoveStack()
    {
        Destroy(objTransform.transform.parent.GetChild(index).gameObject);
        index--;
        objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y - distance, objTransform.transform.position.z);
    }


    int LocateTable()
    {
        int index = -1;
        for (int i = 0; i < tables.Length; i++)
        {
            if (tables[i].gameObject.activeSelf && tables[i].burgerNeeded != 0 && tables[i].isCustomerArrived)
            {
                if (i > 0)
                {
                    if (tables[i - 1].gameObject.activeSelf && tables[i - 1].burgerNeeded != 0 && tables[i - 1].isCustomerArrived)
                    {
                        if (tables[i].burgerNeeded - tables[i].burgerGived > tables[i - 1].burgerNeeded - tables[i - 1].burgerGived)
                        {
                            index = i - 1;
                        }
                    }
                    
                    else if (index == -1)
                    {
                        index = i;
                    }

                }
                else
                {
                    index = i;
                }
            }
        }

        return index;
    }

}

