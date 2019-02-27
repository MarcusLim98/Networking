using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float runSpeed, jumpSpeed;
    protected Joystick js;
    protected JoyButton jb;
    bool jump;
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

        if (!jump &&jb.pressed)
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
