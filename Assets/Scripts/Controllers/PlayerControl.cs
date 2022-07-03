using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Player Movement Scripts 

    PlayerMovement _mover;
    MobileInputs _mobileInput;

    #endregion

    [SerializeField] MineController _mine;

    public bool Hayalet;

    #region Player Movement Bools

    private bool dragStarted;
    private bool isMoving;

    #endregion

    PlayerAnimations _anim;

    private void Awake()
    {
        _mover = GetComponent<PlayerMovement>();
        _mobileInput = new MobileInputs();
        _anim = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        if (_mobileInput.TouchCount > 0)
        {
            if (_mobileInput.touch.phase == TouchPhase.Began)
            {
                isMoving = true;
                _mover.FirstTouch();
            }
        }

        if (isMoving)
        {
            if (_mobileInput.touch.phase == TouchPhase.Moved)
            {
                dragStarted = true;
                _mover.Moving();
            }

            if (_mobileInput.touch.phase == TouchPhase.Ended)
            {
                _mover.Ended();
                isMoving = false;
                dragStarted = false;
            }
        }

        if (dragStarted)
        {
            if (RayTag() == "" || RayTag() == "Door" || Hayalet)
                _mover.Move();

            _mover.Rotate();
        }

        _anim.RunningAnimation(dragStarted);


    }


    string RayTag()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), -transform.forward + -transform.right, out hit, 1f))
        {
            if (!hit.transform.GetComponent<Collider>().isTrigger)
                return hit.collider.tag;

            else
                return "";
        }

        else
            return "";
    }

}
