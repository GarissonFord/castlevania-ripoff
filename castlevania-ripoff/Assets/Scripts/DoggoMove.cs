﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoMove : Enemy {

    public float moveSpeed;
    Rigidbody2D rb;

    Animator anim;

    AnimatorStateInfo currentStateInfo;

    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
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
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.417f);
        Destroy(gameObject);
    }
}