using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] float trashTime;

    bool trash;

    float timer;

    private void Start()
    {
        timer = trashTime;
    }

    private void Update()
    {
        if (trash)
            TrashCan();
    }


    private void OnTriggerEnter(Collider other)
    {
        trash = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trash = false;
    }
    private void TrashCan()
    {
        if (GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = trashTime;
                PlayerStacks.StackInstance.RemoveStack();
            }
        }
    }

}
