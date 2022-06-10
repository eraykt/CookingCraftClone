using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs
{
    public Touch touch => Input.GetTouch(0);
    public int TouchCount => Input.touchCount;
}
