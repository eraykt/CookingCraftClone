using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RunningSpeed();
        CarryAnimations();
    }

    public void RunningAnimation(bool isRunning)
    {
        animator.SetBool("Moving", isRunning);
    }

    public void RunningSpeed()
    {
        animator.SetFloat("Speed", GetComponent<PlayerMovement>().fixedSpeed);
    }

    public void CarryAnimations()
    {
        if (GameManager.Instance.PlayerStack > 0)
        {
            animator.SetBool("isCarrying", true);
        }

        else
        {
            animator.SetBool("isCarrying", false);
        }
    }
}
