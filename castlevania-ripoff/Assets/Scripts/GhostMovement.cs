using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : Enemy {

    //Component references
    Rigidbody2D rb;
    Animator anim;
    public AudioSource audio;

    /* These three variables determine the size and shape of the sine wave
     * pattern that the ghost moves in
     */ 
    public float frequency, magnitude, amplitude;

    Vector3 pos, localScale;

    AnimatorStateInfo currentStateInfo;
    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

    public bool dead;

    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        pos = transform.position;
        localScale = transform.localScale;
        dead = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
        //Without this, the object continues moving while the death animation plays
        if(!dead)
            Move();
	}

    void FixedUpdate()
    {
        if (currentState == deathState)
            rb.velocity = Vector2.zero;
    }

    void Move()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        //This allows the ghost to move in the sine wave pattern
        transform.position = pos + transform.up * amplitude * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void Die()
    {
        audio.Play();
        dead = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("Dead", true);
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.417f);
        Destroy(gameObject);
    }
}
