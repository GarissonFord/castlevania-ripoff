using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Fields that all enemies will have access to

    public SpriteRenderer sr;

    public float moveSpeed;
    public bool facingLeft = true;

    //Health
    public float hitPoint;
    private float maxHitPoint = 100;

    //Damage the enemy deals
    public float damage;

    //So we can call upon the player to take damage once we collide with them
    public PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            pc.SendMessageUpwards("TakeDamage", damage);

        /* Some enemies like skeletons and the dogs are scripted to
         * run in one single direction against the ground. Because
         * they're kinematic rigidbodies, they don't respond to gravity,
         * and as a result, they will walk right over pits. Instead, 
         * colliders will be set up at edges so that the enemies change
         * the direction they're moving once approaching them
         */
        if (collider.gameObject.CompareTag("Edge"))
        {
            moveSpeed = -moveSpeed;
            sr.flipX = !sr.flipX;
        }
    }

    public void TakeDamage(float damage)
    {
        /* We'll get to this later when enemies have actual health
         * 
         */
    }

    public void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
