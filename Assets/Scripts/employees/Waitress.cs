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

    int activeTable = 0;

    float distance = 0.3f;

    bool isWalking, isHolding;

    bool refilled;

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

        if (index == max)
        {
            refilled = true;
        }

        if (LocateTable() != -1)
        {
            if (tables[LocateTable()].burgerNeeded - tables[LocateTable()].burgerGived == index || refilled)
            {
                agent.SetDestination(deliver[LocateTable()].position);

                if (index == 0)
                {
                    refilled = false;
                }
            }
        }

        if (LocateTable() == -1)
        {
            if (index < max)
            {
                agent.SetDestination(bakery.transform.position);
                refilled = false;
            }
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
        int i = 0;

        int mini = -1;

        if (activeTable != tables.Length)
        {
            foreach (TableOrder to in tables)
                if (to.gameObject.activeSelf)
                    activeTable++;
        }


        if (activeTable != 1)
        {
            int[] masalar = new int[activeTable];

            for (i = 0; i < activeTable; i++)
            {
                if (tables[i].isCustomerArrived)
                {
                    masalar[i] = tables[i].burgerNeeded - tables[i].burgerGived;
                }

                else
                {
                    masalar[i] = -1;
                }
            }

            System.Array.Sort(masalar);

            for (i = 0; i < activeTable; i++)
            {
                if (masalar[i] == -1)
                    continue;

                mini = masalar[i];
                break;
            }

            for (i = 0; i < activeTable; i++)
            {
                if (tables[i].isCustomerArrived)
                {
                    if (tables[i].burgerNeeded - tables[i].burgerGived == mini)
                    {
                        index = i;
                    }
                }
            }

        }

        else
        {
            if (tables[0].isCustomerArrived)
                index = 0;
        }

        return index;


        //for (i = 0; i < tables.Length; i++)
        //{
        //    if (tables[i].gameObject.activeSelf && tables[i].burgerNeeded != 0 && tables[i].isCustomerArrived)
        //    {
        //        if (i > 0)
        //        {
        //            if (tables[i - 1].gameObject.activeSelf && tables[i - 1].burgerNeeded != 0 && tables[i - 1].isCustomerArrived)
        //            {
        //                if (tables[i].burgerNeeded - tables[i].burgerGived > tables[i - 1].burgerNeeded - tables[i - 1].burgerGived)
        //                {
        //                    index = i - 1;
        //                }
        //            }

        //            else if (index == -1)
        //            {
        //                index = i;
        //            }

        //        }
        //        else
        //        {
        //            index = i;
        //        }
        //    }
        //}
    }

}

