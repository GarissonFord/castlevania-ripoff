using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public PlayerController pc;
    public float moveSpeed;

    private float hitPoint = 100;
    private float maxHitPoint = 100;
    //Damage the enemy deals
    public float damage;
    public SpriteRenderer sr;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            pc.SendMessageUpwards("TakeDamage", damage);

        if (collider.gameObject.CompareTag("Edge"))
        {
            moveSpeed = -moveSpeed;
            sr.flipX = !sr.flipX;
        }
    }

    public void TakeDamage(float damage)
    {
        //We'll get to this later
    }
}
