using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : Enemy {

    Animator anim;
    Rigidbody2D rb;

    public float moveSpeed, frequency, magnitude;

    Vector3 pos, localScale;

    AnimatorStateInfo currentStateInfo;
    //Current animation state
    static int currentState;
    //Numerical representations of the idle and walk animation states
    static int deathState = Animator.StringToHash("Base Layer.EnemyDeath");

    // Use this for initialization
    void Start ()
    {
        pos = transform.position;
        localScale = transform.localScale;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //0th index is the base layer
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
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
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
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
