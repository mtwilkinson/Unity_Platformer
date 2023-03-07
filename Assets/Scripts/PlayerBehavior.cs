using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour
{
    private float horizontal;
    private int jump = 0;
    public float speed = (float) 5;
    public float accelleration = 1f;
    public float jumpForce = 15f;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask groundLayer;
    public Animator animator;
    public Transform playercoord;
    public Vector2 respawn;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (IsGrounded()) {
            if (Input.GetButtonDown("Jump")) {
                jump = 1;
                print("jump");
            }
        } else if (leftWall()) {
            if (Input.GetButtonDown("Jump")) {
                jump = 2;
                print("left");
            }
        } else if (rightWall()) {
            if (Input.GetButtonDown("Jump")) {
                jump = 3;
                print("right");
            }
        }   
        
        animator.SetFloat("xSpeed", Math.Abs(rb.velocity.x));
        animator.SetFloat("xVelocity", rb.velocity.x);
        if (playercoord.position.y < -20) {
            death();
        } 
    }

    private void death() {
        playercoord.SetPositionAndRotation(new Vector3(respawn.x, respawn.y, 0), new Quaternion(0,0,0,0));
    }

    private bool IsGrounded()
    {
        bool ground = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("onGround", ground);
        return ground;
    }
    private bool leftWall() {
        return Physics2D.OverlapCircle(leftCheck.position, 0.2f, groundLayer);
    }
    private bool rightWall() {
        return Physics2D.OverlapCircle(rightCheck.position, 0.2f, groundLayer);
    }


    void FixedUpdate()
    {
        if ((rb.velocity.x < speed && horizontal > 0) || (rb.velocity.x > -speed && horizontal < 0)) {
            rb.AddForce(new Vector3(horizontal * accelleration, 0, 0), ForceMode2D.Impulse);
        }
        if (jump == 1) {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
            jump = 0;
            animator.SetTrigger("jump");
        } else if (jump == 2) {
            rb.velocity = new Vector2(0f,0f);
            rb.AddForce(new Vector3(0.5f * jumpForce, 0.85f * jumpForce, 0), ForceMode2D.Impulse);
            jump = 0;
            animator.SetTrigger("jump");
        } else if (jump == 3) {
            rb.velocity = new Vector2(0f,0f);
            rb.AddForce(new Vector3(-0.5f * jumpForce, 0.85f * jumpForce, 0), ForceMode2D.Impulse);
            jump = 0;
            animator.SetTrigger("jump");
        }
    }
}
