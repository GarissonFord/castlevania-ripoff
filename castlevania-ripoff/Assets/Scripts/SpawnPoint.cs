using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject enemyToSpawn;
    public Transform whereToSpawn;
    public bool spawned;
    //public bool playerInRange;

    /* Residual code from when this prototype was going to have
     * enemies spawning at regular intervals from these points
     * 
     * */
    //public float timeSinceLastSpawn, timeBetweenSpawns;

    private void Awake()
    {
        spawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!spawned)
            Instantiate(enemyToSpawn, whereToSpawn);
        spawned = true;
    }

    /* Same down here 
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }
    */
}
