using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    //we serialize the fields so they can be seen in the unity editor inspector window
    //float movement stores the input, SPEED can be changed to make movement faster or slower
    //the booleans check where the character is facing and if it is grounded to determine if it can jump or should be flipped
    //set SPEED to 5 rather than 15 to slow down the character
    [SerializeField] public float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 5;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce = 350.0f;
    [SerializeField] bool isGrounded = true;

    [SerializeField] Animator animator;

    const int IDLE = 0;
    const int RUN = 1;
    const int JUMP = 2;

    // Start is called before the first frame update
    //if the archer doesn't have a rigid body we make sure it gets one
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetInteger("motion", IDLE);
    }

    // Update is called once per frame
    //we dont use this for physics and movement because it is tied to frame rate
    //instead, we are primarily using it here for getting user input
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    //FixedUpdate may be called many times per frame
    //We will use it for movement
    private void FixedUpdate()
    {
        //assigns a new velocity to the rigid body using the movement from Update and existing y velocity
        rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);

        //we check if the sprite has to be flipped and flip it if it does
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();

        //we check if the jump key is pressed and the sprite is on the ground, and make it jump if it does
        if (jumpPressed && isGrounded)
            Jump();
        else {
            jumpPressed = false;
            if (isGrounded)
            {
                if (movement > 0 || movement < 0)
                {
                    animator.SetInteger("motion", RUN);
                }
                else
                {
                    animator.SetInteger("motion", IDLE);
                }
            }
        }

    }

        //method to flip the sprite based on where it is facing
        private void Flip()
        {
            //flip the sprite and record that in the boolean
            transform.Rotate(0, 180, 0);
            isFacingRight = !isFacingRight;
        }

    //method for the sprite to "jump", is called by fixedupdate only when the sprite meets the proper conditions
    private void Jump()
    {
        animator.SetInteger("motion", JUMP);
        //set the y velocity to 0 and then increase it based off the jump force
        //animator.SetInteger("motion", JUMP);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        jumpPressed = false;
        isGrounded = false;
    }

    //method to set is grounded to true on collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
        animator.SetInteger("motion", IDLE);
    }
}
