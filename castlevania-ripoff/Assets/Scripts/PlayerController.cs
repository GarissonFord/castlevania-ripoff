using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Used for the Flip() function
    public bool facingRight = true;

    //Tells us when the player is airborne 
    public bool jump;

    public bool crouching;
    
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

    /* Okay, so what I need to do is set the player's velocity to zero
     * when they are in the crouching and attack animations. This will
     * involve code from the old beat-em up game I made about a year 
     * ago where I can monitor animation states
     * 
     * The following is some of that code.
     */

    AnimatorStateInfo currentStateInfo;

    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int idleState = Animator.StringToHash("Base Layer.PlayerIdle");
    static int walkState = Animator.StringToHash("Base Layer.PlayerRun");
    static int attackState = Animator.StringToHash("Base Layer.PlayerAttack");
    static int jumpState = Animator.StringToHash("Base Layer.PlayerJump");
    static int crouchState = Animator.StringToHash("Base Layer.PlayerCrouch");
    static int hurtState = Animator.StringToHash("Base Layer.PlayerHurt");

    /* Okay now what I need to do is create hitboxes 
     * that are only active when a specific frame is
     * being rendered
     */

    //Our hitbox
    public GameObject attackHitBox;

    //The sprite that our attack hitbox will be tied to
    public Sprite attackHitSprite;

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

        //'Animates' the crouch

        if (Input.GetButtonDown("Crouch"))
        {
            crouching = true;
            anim.SetBool("Crouching", true);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            crouching = false;
            anim.SetBool("Crouching", false);
        }

        //Just to test the hurt animation, Fire2 is set to k
        if (Input.GetButtonDown("Fire2"))
            anim.SetTrigger("Hurt");

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

        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;

        //Player shouldn't be moving while attacking
        /*
        if (currentState == attackState)
            Debug.Log("Attacking");

        if (currentState == crouchState)
            Debug.Log("Crouching");
        */

        //Fire3 is set to t
        if (Input.GetButtonDown("Fire3"))
            Die();
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

        //The following three animation states should not allow a player to move while 
        //they are the current state

        if (crouching)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = rb.velocity;

        if (currentState == attackState)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = rb.velocity;

        if (currentState == hurtState)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = rb.velocity;

        //Testing if the right sprite is being rendered
        if (attackHitSprite == sr.sprite)
        //If so, the hitbox is set to active and it can damage an enemy       
            attackHitBox.gameObject.SetActive(true);
        else
            attackHitBox.gameObject.SetActive(false);

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

    public void Die()
    {
        sr.color = Color.red;
        //Destroy(gameObject.GetComponent<Collider2D>());
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}



