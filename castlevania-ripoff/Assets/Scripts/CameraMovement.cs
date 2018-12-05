using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 offset;         //Private variable to store the offset distance between the player and camera

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
}
