using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    MobileInputs _mobileinput;

    Vector2 firstTouch;
    Vector2 lastTouch;

    bool isDragging;
    bool isMoving;

    private void Awake()
    {
        _mobileinput = new MobileInputs();
    }

    private void Update()
    {
        if (_mobileinput.TouchCount > 0)
        {
            if (_mobileinput.touch.phase == TouchPhase.Began)
            {
                firstTouch = _mobileinput.touch.position;
                lastTouch = _mobileinput.touch.position;
                gameObject.transform.position = _mobileinput.touch.position;
            }
        }

        if (isMoving)
        {
            if (_mobileinput.touch.phase == TouchPhase.Moved)
            {
                isDragging = true;
                lastTouch = _mobileinput.touch.position;
            }
            if (_mobileinput.touch.phase == TouchPhase.Ended)
            {
                lastTouch = _mobileinput.touch.position;
                lastTouch = Vector3.zero;
                firstTouch = Vector3.zero;

                isDragging = false;
                isMoving = false;
            }

        }

        if (isDragging)
        {
            Vector3 vector2 = (lastTouch - firstTouch).normalized;
            transform.GetChild(0).transform.Translate(vector2 - transform.GetChild(0).transform.position);
        }
    }
}
