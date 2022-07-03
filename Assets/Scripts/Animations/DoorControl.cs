using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    HingeJoint joint;
    float angle;
    JointMotor motor;

    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
    }

    private void Start()
    {
        motor = joint.motor;
    }

    private void Update()
    {
        angle = joint.angle;
        motor.targetVelocity = -angle;
        joint.motor = motor;
    }

}
