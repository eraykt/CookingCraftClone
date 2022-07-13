using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waitress : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] BakeryOut hotdog;
    [SerializeField] BakeryOut pizza;


    [SerializeField] TableOrder[] tables;
    [SerializeField] Transform[] deliver;

    public Transform objTransform;


    int activeTable = 0;

    float distance = 0.3f;

    bool isWalking, isHolding;

    bool refilled;

    public int index, max = 4;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (index == 0 && pizza.cookedIndex <= hotdog.cookedIndex)
        {
            agent.SetDestination(hotdog.transform.position);
        }

        else if (index == 0 && hotdog.cookedIndex < pizza.cookedIndex)
        {
            agent.SetDestination(pizza.transform.position);
        }

        if (index == max)
        {
            refilled = true;
        }

        if (LocateTable() != -1)
        {
            if (objTransform.parent.GetChild(index).CompareTag("Pizza"))
            {
                if ((tables[LocateTable()].pizzaNeeded - tables[LocateTable()].pizzaGived == index && index != 0) || refilled)
                {
                    agent.SetDestination(deliver[LocateTable()].position);

                    if (index == 0)
                    {
                        refilled = false;
                    }
                }
            }

            else
            {
                if ((tables[LocateTable()].hotdogNeeded - tables[LocateTable()].hotdogGived == index && index != 0) || refilled)
                {
                    agent.SetDestination(deliver[LocateTable()].position);

                    if (index == 0)
                    {
                        refilled = false;
                    }
                }
            }
        }

        if (LocateTable() == -1)
        {
            if (index < max)
            {
                if (pizza.cookedIndex < hotdog.cookedIndex)
                {
                    agent.SetDestination(pizza.transform.position);
                    refilled = false;
                }

                else if (hotdog.cookedIndex >= pizza.cookedIndex)
                {
                    agent.SetDestination(pizza.transform.position);
                    refilled = false;
                }
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

        int miniH = -1;
        int miniP = -1;

        if (activeTable != tables.Length)
        {
            foreach (TableOrder to in tables)
                if (to.gameObject.activeSelf)
                    activeTable++;
        }


        if (activeTable != 1)
        {
            int[] hotdogMasa = new int[activeTable];
            int[] pizzaMasa = new int[activeTable];

            for (i = 0; i < activeTable; i++)
            {
                if (tables[i].isCustomerArrived)
                {
                    hotdogMasa[i] = tables[i].hotdogNeeded - tables[i].hotdogGived;
                    pizzaMasa[i] = tables[i].pizzaNeeded - tables[i].pizzaGived;
                }

                else
                {
                    hotdogMasa[i] = -1;
                    pizzaMasa[i] = -1;
                }
            }

            System.Array.Sort(hotdogMasa);
            System.Array.Sort(pizzaMasa);

            for (i = 0; i < activeTable; i++)
            {
                if (hotdogMasa[i] == -1 || hotdogMasa[i] == 0)
                    continue;

                miniH = hotdogMasa[i];
                break;
            }

            for (i = 0; i < activeTable; i++)
            {
                if (pizzaMasa[i] == -1 || pizzaMasa[i] == 0)
                    continue;

                miniP = pizzaMasa[i];
                break;
            }

            if (objTransform.parent.GetChild(this.index).CompareTag("Sosisli"))
            {
                for (i = 0; i < activeTable; i++)
                {
                    if (tables[i].isCustomerArrived)
                    {
                        if (tables[i].hotdogNeeded - tables[i].hotdogGived == miniH)
                        {
                            index = i;
                        }
                    }
                }
            }

            else
            {
                for (i = 0; i < activeTable; i++)
                {
                    if (tables[i].isCustomerArrived)
                    {
                        if (tables[i].pizzaNeeded - tables[i].pizzaGived == miniP)
                        {
                            index = i;
                        }
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

