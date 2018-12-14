using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //This script covers the movement of the camera during gameplay, NOT the title screen

    public GameObject player;
    //Private variable to store the offset distance between the player and camera
    private Vector2 offset;        

    void Start()
    {
        /* Calculate and store the offset value by getting the distance between the 
         * player's position and camera's position.
         */
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //For now I guess the offset just doesn't matter
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
