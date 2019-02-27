using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float runSpeed, jumpSpeed;
    protected Joystick js;
    protected JoyButton jb;
    bool jump, canJump = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        js = FindObjectOfType<Joystick>();
        jb = FindObjectOfType<JoyButton>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(js.Horizontal * runSpeed, rb.velocity.y, js.Vertical * runSpeed);

        Vector3 lookDirection = new Vector3(js.Horizontal, 0, js.Vertical );
        transform.rotation = Quaternion.LookRotation(lookDirection);

        if (canJump)
        {
            if (!jump && jb.pressed)
            {
                jump = true;
                rb.velocity += Vector3.up * jumpSpeed;
            }
            else if (jump && jb.pressed)
            {
                jump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            canJump = false;
        }
    }
}
