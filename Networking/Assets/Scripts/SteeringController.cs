using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SteeringController : MonoBehaviour
{
    public SimpleCarController simpleCarController;
    bool accelerate, reverse;
    public void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Accelerate()
    {
        if (!accelerate)
        {
            simpleCarController.motorForce = 200;
            reverse = false;
            accelerate = true;
        }
        else if (accelerate)
        {
            simpleCarController.motorForce = 0;
            accelerate = false;
        }
    }

    public void Reverse()
    {
        if (!reverse)
        {
            simpleCarController.motorForce = -200;
            reverse = true;
            accelerate = false;
        }
        else if (reverse)
        {
            simpleCarController.motorForce = 0;
            reverse = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Neutral")
        {
            simpleCarController.horizontalInput = 0;
        }
        if (other.name == "Right1")
        {
            simpleCarController.horizontalInput = 0.20f;
        }
        if (other.name == "Right2")
        {
            simpleCarController.horizontalInput = 0.35f;
        }
        if (other.name == "Right3")
        {
            simpleCarController.horizontalInput = 0.45f;
        }
        if (other.name == "Left1")
        {
            simpleCarController.horizontalInput = -0.20f;
        }
        if (other.name == "Left2")
        {
            simpleCarController.horizontalInput = -0.35f;
        }
        if (other.name == "Left3")
        {
            simpleCarController.horizontalInput = -0.45f;
        }
    }
}
