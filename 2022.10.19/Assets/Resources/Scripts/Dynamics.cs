using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamics : MonoBehaviour
{
    public float JumpPower = 50f;

    public float Speed = 5f;

    public bool bJump = false;

    public bool bSit = false;


    Rigidbody2D rigb;


    Animator Ani;


    // Start is called before the first frame update
    void Start()
    {
        rigb = this.GetComponent<Rigidbody2D>();
        Ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bSit)
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * Speed * Time.deltaTime);
                this.GetComponent<SpriteRenderer>().flipX = true;
                if (bJump)
                {
                    Ani.Play("Run");
                }
                else
                {
                    Ani.Play("Jump");
                }

            }
            if (Input.GetKeyDown(KeyCode.W) && bJump)
            {
                rigb.AddForce(Vector2.up * JumpPower);
                Ani.Play("Jump");
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * Speed * Time.deltaTime);
                this.GetComponent<SpriteRenderer>().flipX = false;
                if (bJump)
                {
                    Ani.Play("Run");
                }
                else
                {
                    Ani.Play("Jump");
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            bSit = true;
            Ani.Play("Crouch");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Ani.Play("Idle");
            bSit = false;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==6)
        {
            Ani.Play("Idle");
            bJump = true;
        }

        if (collision.gameObject.layer == 8)
        {
            if (!bJump)
            {
                rigb.AddForce(Vector2.up * JumpPower);
                Ani.Play("Jump");
            }
            
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            bJump = false;
        }
    }
}
