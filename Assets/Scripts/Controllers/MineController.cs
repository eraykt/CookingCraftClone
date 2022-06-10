using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public int index { get; set; }
    public bool isCollecting { get; private set; }

    public float speed;
    public float CollectingSpeed;

    Coroutine generator, collecting, stacks;

    [SerializeField] PlayerControl player;

    private void Start()
    {
        generator = StartCoroutine(Generator());
    }


    IEnumerator Generator()
    {
        while (index < transform.childCount)
        {
            yield return new WaitForSeconds(speed);
            transform.GetChild(index).gameObject.SetActive(true);
            index++;
            yield return null;
        }
    }

    IEnumerator Collecting()
    {
        isCollecting = true;
        while (index > 0)
        {
            yield return new WaitForSeconds(CollectingSpeed);
            index--;
            transform.GetChild(index).gameObject.SetActive(false);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.index < player.maxStack)
            {
                StopCoroutine(generator);
                collecting = StartCoroutine(Collecting());
                stacks = StartCoroutine(player.Stack());
            }

            else
            {
                StopCoroutine(collecting);
                StopCoroutine(stacks);
                generator = StartCoroutine(Generator());
                isCollecting = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.name}");
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(collecting);
            StopCoroutine(stacks);
            generator = StartCoroutine(Generator());
            isCollecting = false;
        }

    }
}
