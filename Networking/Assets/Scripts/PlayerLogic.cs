using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float runSpeed, jumpSpeed;
    protected Joystick js;
    protected JoyButton jb;
    float xzAxis, yAxis;
    bool jump, canJump = true, canMove = false;
    Rigidbody rb;
    Animator anim;
    float oldPosX;
    // Start is called before the first frame update
    void Start()
    {
        js = FindObjectOfType<Joystick>();
        jb = FindObjectOfType<JoyButton>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(js.Horizontal * xzAxis, rb.velocity.y, js.Vertical * xzAxis);

        Vector3 lookDirection = new Vector3(js.Horizontal, 0, js.Vertical );
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookDirection, 3f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (rb.velocity.magnitude < 0)
        {
            anim.SetInteger("State", 1);
        }
        else if (rb.velocity.magnitude > 0)
        {
            anim.SetInteger("State", 2);
        }

        if (canJump)
        {
            if (!jump && jb.pressed)
            {
                jump = true;
                rb.velocity += Vector3.up * yAxis;
            }
            else if (jump && jb.pressed)
            {
                jump = false;
            }
        }
    }

    public void StopMove()
    {
        xzAxis = 0;
        yAxis = 0;
    }

    public void CanMove()
    {
        anim.SetInteger("State", 1);
        xzAxis = runSpeed;
        yAxis = jumpSpeed;
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
