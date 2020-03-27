using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    ////////////////////////////////
    //Everything Physics related //
    //////////////////////////////

    //Our GetAxis value for movement
    public float h;

    //i.e. Speed
    public float moveForce;
    public float maxSpeed;

    //Power of a jump
    public float jumpForce;

    //To check when the player is touching the ground
    public Transform groundCheck;
    public bool grounded;

    //Tells us when the player is airborne 
    public bool jump;

    //https://www.youtube.com/watch?v=7KiK0Aqtmzc
    //Allows the player to fall faster than the physics engine normally would allow
    public float fallMultiplier;
    public float lowJumpMultiplier;

    //The hitbox for the player's attack
    public GameObject attackHitBox;

    /////////////////////////////
    //Everything for Animation//
    ///////////////////////////

    AnimatorStateInfo currentStateInfo;
    //Integer that will represent the current animation state
    static int currentState;
    //Integer representations of all of the player's animations
    static int idleState = Animator.StringToHash("Base Layer.PlayerIdle");
    static int walkState = Animator.StringToHash("Base Layer.PlayerRun");
    static int attackState = Animator.StringToHash("Base Layer.PlayerAttack");
    static int jumpState = Animator.StringToHash("Base Layer.PlayerJump");
    static int crouchState = Animator.StringToHash("Base Layer.PlayerCrouch");
    static int hurtState = Animator.StringToHash("Base Layer.PlayerHurt");

    //The sprite that our attack hitbox will be tied to
    public Sprite attackHitSprite;
    //the sprite
    public Sprite deathSprite;

    //Used for the Flip() function
    public bool facingRight = true;

    //Boolean to let the player crouch frames be rendered
    public bool crouching;
    
    /////////////////////////
    //Everything for Audio//
    ///////////////////////
    public AudioClip attackAudioClip, hurtAudioClip, deathAudioClip;
    AudioSource audio;

    ///////////////////////////////////////////////////////////////////////////////////////////
    //The following are miscellaneous variables that contributes to the overall game's logic//
    /////////////////////////////////////////////////////////////////////////////////////////

    public float hitPoint = 100;
    private float maxHitPoint = 100;
    //How long a player can't move when they are hit
    public float stunLockTime;
    //References to the graphical representations of the player's health
    public Image currentHealthBar;
    public Text ratioText;
    public bool dead;
    /* The player will briefly be invincible after being hurt and this bool checks if the player 
     * can take damage again
     */ 
    public bool vulnerable;

    //References to objects/scripts not attached to the player
    public PlayerController pc;
    public GameController gameController;
    public GameOverMenuScript goms;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        UpdateHealthbar();
        pc = GetComponent<PlayerController>();
        gameController = FindObjectOfType<GameController>();
        dead = false;
        vulnerable = true;
        goms = FindObjectOfType<GameOverMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Uses a vertical linecast to see if the player is touching the ground
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded)
        {
            jump = false;
            anim.SetBool("Grounded", true);
        }

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
        {
            audio.clip = attackAudioClip;
            audio.Play();
            anim.SetTrigger("Attack");
        }

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

        // Jump code comes from the following video: https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Updates the current state
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
    }

    void FixedUpdate()
    {
        //If not checking for death, then the player could still move even in the death animation
        if (!dead)
        {
            //Can range from -1 to 1, 0 means the player is not moving
            h = Input.GetAxis("Horizontal");

            //The player is moving if h isn't 0, so we set to a static velocity
            if (h != 0)
                rb.velocity = new Vector2(h * moveForce, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && !jump)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jump = true;
                anim.SetBool("Grounded", false);
            }

            //The following three animation states should not allow a player to move while 
            //they are the current state

            if (crouching)
                rb.velocity = Vector2.zero;

            if (currentState == attackState)
                rb.velocity = Vector2.zero;

            if (currentState == hurtState)
                rb.velocity = Vector2.zero;

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
    }

    //Changes rotation of the player
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    /*
    public void Damage()
    {
        audio.clip = hurtAudioClip;
        audio.Play();
        StartCoroutine(DamageFlash());
        anim.SetTrigger("Damage");
    }
    */

    //Player taking damage
    private void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            hitPoint -= damage;
            UpdateHealthbar();
            //Like any game, if the thing reaches 0 hit points, it's done

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Die();
            }
            else
            {
                //If not though, just take damage and animation accordingly
                anim.SetTrigger("Damage");
                audio.clip = hurtAudioClip;
                audio.Play();
                StartCoroutine(DamageFlash());
            }
        }
    }

    //The player is frozen in place and flashes red to indicate they took damage
    private IEnumerator DamageFlash()
    {
        rb.velocity = Vector2.zero;
        sr.color = Color.red;
        vulnerable = false;
        yield return new WaitForSeconds(stunLockTime);
        vulnerable = true;
        sr.color = Color.white;
    }

    public void UpdateHealthbar()
    {
        float ratio = hitPoint / maxHitPoint;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0");
    }


    public void Die()
    {
        dead = true;
        audio.clip = deathAudioClip;
        audio.Play();
        goms.GameOver();
        rb.velocity = Vector2.zero;
        audio.clip = deathAudioClip;
        audio.Play();
        sr.color = Color.red;
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        sr.sprite = deathSprite;
        sr.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}



