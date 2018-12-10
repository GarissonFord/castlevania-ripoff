using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public PlayerController pc;

    private float hitPoint = 100;
    private float maxHitPoint = 100;
    //Damage the enemy deals
    public float damage;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            pc.SendMessageUpwards("TakeDamage", damage);
    }

    public void TakeDamage(float damage)
    {
        //We'll get to this later
    }
}
