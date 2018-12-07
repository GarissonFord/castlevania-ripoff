using System.Collections;
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
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

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

        if (currentState == deathState)
            rb.velocity = Vector2.zero;
    }

    public void Die()
    {
        anim.SetBool("Dead", true);
        Destroy(gameObject.GetComponent<Collider2D>());
        StartCoroutine(KillOnAnimationEnd());
    }

    //Uses time from the enemy death animation
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.417f);
        Destroy(gameObject);
    }
}
