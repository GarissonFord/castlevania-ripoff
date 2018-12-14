using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoMove : Enemy {

    //public float moveSpeed;
    Rigidbody2D rb;

    Animator anim;

    public AudioSource audio;

    //Lets us know the which animation state is current playing
    AnimatorStateInfo currentStateInfo;
    //int representing the state
    static int currentState;
    
    //The run animation is always playing so we only need a reference to the death animation
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

    //We'll need this to prevent the enemy from moving while their death animation plays
    public bool dead;

    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        dead = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
        //Dog is moving as long as they aren't dead
        if(!dead)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
	}

    private void FixedUpdate()
    {
        if (currentState == deathState)
            rb.velocity = Vector2.zero;            
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Dead", true);
        audio.Play();
        dead = true;
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.417f);
        Destroy(gameObject);
    }
}
