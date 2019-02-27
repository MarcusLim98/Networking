using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerLogic : MonoBehaviour
{
    public GameObject downAttack, rightAttack, leftAttack;
    public float runSpeed, jumpSpeed;
    protected Joystick js, js2;
    float xzAxis, yAxis, hp, maxHp = 100f; 
    bool jump, canJump = true, canMove = false, attack;
    Image healthBar;    
    Rigidbody rb;
    Animator anim;
    float oldPosX;
    // Start is called before the first frame update
    void Start()
    {
        js = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        js2 = GameObject.Find("Fixed Joystick 2").GetComponent<Joystick>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(js.Horizontal * xzAxis, rb.velocity.y, js.Vertical * xzAxis);

        Vector3 lookDirection = new Vector3(js.Horizontal, 0, js.Vertical );
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookDirection, 3f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        //print(js.Vertical);
        if(js2.Vertical >= 0.5f)
        {
            //print("up");
            if (canJump)
            {
                if (!jump)
                {
                    jump = true;
                    rb.velocity += Vector3.up * yAxis;
                }
                else if (jump)
                {
                    jump = false;
                }
            }
        }
        else if (js2.Vertical <= -0.5f && canJump && !attack)
        {
            //print("down");
            attack = true;
            anim.SetInteger("State", 3);
        }
        else if (js2.Horizontal >= 0.5f && canJump && !attack)
        {
            //print("right");
            attack = true;
            anim.SetInteger("State", 4);
        }
        else if (js2.Horizontal <= -0.5f && canJump && !attack)
        {
            //print("left");
            attack = true;
            anim.SetInteger("State", 5);
        }
        //print(js.Horizontal);

        if (rb.velocity.magnitude < 0 && !attack)
        {
            anim.SetInteger("State", 1);
        }
        else if (rb.velocity.magnitude > 0 && !attack)
        {
            anim.SetInteger("State", 2);
        }

        healthBar.fillAmount = hp / maxHp;
        if(healthBar.fillAmount <= 0)
        {
            NetworkManager.singleton.StopHost();
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

    public void AttackOver()
    {
        attack = false;
        xzAxis = runSpeed;
        yAxis = jumpSpeed;
        anim.SetInteger("State", 1);
    }

    public void ShootBall()
    {
        Instantiate(downAttack, transform.position, Quaternion.identity);
    }

    public void Thunder()
    {
        Instantiate(rightAttack, new Vector3(0, 9, transform.position.z + 10), Quaternion.Euler(90, 0, 0));
    }

    public void Spam()
    {
        Instantiate(leftAttack, transform.position, Quaternion.identity);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Damage")
        {
            hp -= 10;
        }
    }
}
