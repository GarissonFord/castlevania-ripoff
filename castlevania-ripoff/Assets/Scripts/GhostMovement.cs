using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : Enemy {

    Animator anim;
    Rigidbody2D rb;

    public float /*moveSpeed*/ frequency, magnitude, amplitude;

    Vector3 pos, localScale;

    AnimatorStateInfo currentStateInfo;
    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

    public AudioSource audio;

    public bool dead;

    //public SpriteRenderer sr;

    // Use this for initialization
    void Awake ()
    {
        pos = transform.position;
        localScale = transform.localScale;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        dead = false;
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
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
