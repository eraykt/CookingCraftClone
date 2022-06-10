using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerMovement _mover;
    MobileInputs _mobileInput;
    Rigidbody rig;

    private bool dragStarted;
    private bool isMoving;


    private void Awake()
    {
        _mover = GetComponent<PlayerMovement>();
        _mobileInput = new MobileInputs();
        rig = GetComponent<Rigidbody>();
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
            _mover.Move();
            _mover.Rotate();
        }
    }

   

    
}
