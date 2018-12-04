using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Used for the Flip() function
    public bool facingRight = true;

    //Tells us when the player is airborne 
    public bool jump;

    //i.e. Speed
    public float moveForce;
    public float maxSpeed;

    //Power of a jump
    public float jumpForce;

    //https://www.youtube.com/watch?v=7KiK0Aqtmzc
    public float fallMultiplier;
    public float lowJumpMultiplier;

    //Rigidbody2D reference
    private Rigidbody2D rb;

    //To check when the player is touching the ground
    public Transform groundCheck;
    public bool grounded;

    //Our GetAxis value for movement
    public float h;

    public SpriteRenderer sr;
    Animator anim;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Uses a vertical linecast to see if the player is touching the ground
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded)
            anim.SetBool("Grounded", true);

        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("IsMoving", true);
        }

        //Stops walk-cycle
        if(Input.GetButtonUp("Horizontal"))
        {
            anim.SetBool("IsMoving", false);
        }

        if (Input.GetButtonDown("Attack"))
            anim.SetTrigger("Attack");

        //jump code comes from the following video https://www.youtube.com/watch?v=7KiK0Aqtmzc
        //allows player to fall faster
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //Represents if player is moving in the positive or negative direction
        h = Input.GetAxis("Horizontal");

        //The player is moving if h isn't 0, so we set to a static velocity
        if (h != 0)
            rb.velocity = new Vector2(h * moveForce, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Grounded", false);
        }

        //Flips when hitting 'right' and facing left
        if (h > 0 && !facingRight)
            Flip();
        //Flips when hitting 'left' and facing right
        else if (h < 0 && facingRight)
            Flip();
    }

    //Changes rotation of the player
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}



