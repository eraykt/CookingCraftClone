using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MobileInputs _mobileInputs;

    private Vector3 touchDown;
    private Vector3 touchUp;


    [SerializeField] private float movementSpeed = 3;
    [SerializeField] private float rotationSpeed = 300f;

    private void Awake()
    {
        _mobileInputs = new MobileInputs();
    }

    public void FirstTouch()
    {
        touchDown = _mobileInputs.touch.position;
        touchUp = _mobileInputs.touch.position;
    }

    public void Moving()
    {
        touchDown = _mobileInputs.touch.position;
    }

    public void Ended()
    {
        touchDown = _mobileInputs.touch.position;
        touchDown = Vector3.zero;
        touchUp = Vector3.zero;
    }

    public void Move()
    {
        gameObject.transform.Translate(new Vector3(-1, 0, -1) * movementSpeed * Time.deltaTime);
    }

    public void Rotate()
    {
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);
    }

    Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);

        return temp;
    }

    Vector3 CalculateDirection()
    {
        Vector3 temp = (touchDown - touchUp).normalized;
        temp.z = temp.y;
        temp.y = 0;
        return temp;
    }
}
