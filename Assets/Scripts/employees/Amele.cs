using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Amele : MonoBehaviour
{
    public static Amele instance { get; private set; }

    [SerializeField] MineController mine;
    [SerializeField] BakeryIn[] bakery;

    bool isWalking;
    bool isHolding;

    NavMeshAgent agent;


    [SerializeField] Transform objTransform;
    [SerializeField] float distance = 0.3f;

    public int limit, holding;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //if (bakery.Length == 1)
        //{
        //    if (mine.index > 0 && bakery[0].current < bakery[0].max && holding == 0)
        //    {
        //        agent.SetDestination(mine.transform.position);
        //    }

        //    if (holding == limit && bakery[0].current < bakery[0].max)
        //    {
        //        agent.SetDestination(bakery[0].transform.GetChild(0).transform.position);
        //    }
        //}

        //else
        //{
        //if (LocateBakery() != -1)
        //{
        if (holding == 0 && mine.index > 0)
        {
            agent.SetDestination(mine.transform.position);
        }

        if (holding == limit && bakery[LocateBakery()].current < bakery[LocateBakery()].max)
        {
            agent.SetDestination(bakery[LocateBakery()].transform.GetChild(0).transform.position);

        }
        //}
        //}

        isWalking = agent.velocity != Vector3.zero;
        isHolding = holding > 0;

        GetComponent<Animator>().SetBool("walk", isWalking);
        GetComponent<Animator>().SetBool("isHolding", isHolding);



    }
    public IEnumerator AddStack(int stackIndex)
    {
        while (holding < limit)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);
            GameObject go = Instantiate(PlayerStacks.StackInstance.stackPrefabs[stackIndex], new Vector3(objTransform.transform.position.x, objTransform.transform.position.y, objTransform.transform.position.z), PlayerStacks.StackInstance.stackPrefabs[stackIndex].transform.rotation, objTransform.parent.transform);
            go.transform.rotation = objTransform.transform.rotation;
            objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y + distance, objTransform.transform.position.z);
            holding++;
            yield return null;
        }
    }

    public void RemoveStack()
    {
        Destroy(objTransform.transform.parent.GetChild(holding).gameObject);
        holding--;
        objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y - distance, objTransform.transform.position.z);
    }

    int LocateBakery()
    {
        if (holding == limit)
        {
            if (bakery[1].current < bakery[0].current)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }

        return -1;
    }
}
