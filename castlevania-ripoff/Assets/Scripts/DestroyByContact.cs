using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private void Start()
    {
        //sm = FindObjectOfType<SkeletonMovement>();
    }
    //Destroys an object if it's an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))       
            collision.SendMessageUpwards("Die");
    }
}
