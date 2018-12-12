using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCamera : MonoBehaviour {

    public float moveSpeed;
    Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * moveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = Vector2.right * moveSpeed;
    }
}
