using UnityEngine;
using UnityEngine.InputSystem;

public class HingeController : MonoBehaviour
{
    public HingeJoint2D joint;
    public float contractMotorStrength;


    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            // Contract
            if (mouse.leftButton.isPressed)
            {
                JointMotor2D motor = joint.motor;
                motor.motorSpeed = contractMotorStrength;
                joint.motor = motor;
            }
            // Relax
            else
            {
                JointMotor2D motor = joint.motor;
                motor.motorSpeed = contractMotorStrength * -1;
                joint.motor = motor;
            }
        }
    }
}
