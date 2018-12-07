using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject enemyToSpawn;
    public bool playerInRange;

    public float timeSinceLastSpawn, timeBetweenSpawns;

    public void Update()
    {
        //If player is close enough
        if (playerInRange)
        {
            //Start spawning enemies incrementally
            if (Time.time - timeSinceLastSpawn >= timeBetweenSpawns)
            {
                Instantiate(enemyToSpawn, new Vector3(transform.position.x, -0.05f, 0.0f), gameObject.transform.rotation);
                timeSinceLastSpawn = Time.time;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }
}
