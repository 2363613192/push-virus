using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    bool IsGround;

    private Rigidbody2D myRb2;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    
    void Start()
    {
        myRb2 = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Run();
        Jump();
        CheckGrounded();
        SwitchAnimation();
    }

    private void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        myRb2.velocity = new Vector2(moveDir * runSpeed, myRb2.velocity.y);
        //bool hasXSpeed =  Mathf.Abs(myRb2.velocity.x) > 0;  // Mathf.Epsilon是一个很小的数
        //myAnim.SetBool("Run", hasXSpeed);
        if (moveDir != 0) 
        {
            myAnim.SetBool("Run", true);
            if (moveDir > 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else
                GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            myAnim.SetBool("Run", false);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGround)
            {
                myRb2.velocity = new Vector2(myRb2.velocity.x, jumpSpeed);
                myAnim.SetBool("Jump", true);
            }
        }
    }

    private void CheckGrounded()
    {
        IsGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRb2.velocity.y < 0)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
            else if (IsGround)
            {
                myAnim.SetBool("Fall", false);
                myAnim.SetBool("Idle", true);
            }
        }
    }
}
