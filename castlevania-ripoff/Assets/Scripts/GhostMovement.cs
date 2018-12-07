using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;

    public float moveSpeed, frequency, magnitude;

    Vector3 pos, localScale;

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
        Move();
	}

    void Move()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void Die()
    {
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
