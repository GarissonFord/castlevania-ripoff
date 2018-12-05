﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour {

    Rigidbody2D rb;

    public float moveSpeed;

    public SpriteRenderer sr;
    Animator anim;

    AnimatorStateInfo currentStateInfo;

    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int riseState = Animator.StringToHash("Base Layer.SkeletonRise");
    static int walkState = Animator.StringToHash("Base Layer.SkeletonWalk");

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
	}

    void FixedUpdate()
    {
        if (currentState == riseState)
            rb.velocity = Vector2.zero;

        if (currentState == walkState)
            rb.velocity = Vector2.right * moveSpeed;
    }
}