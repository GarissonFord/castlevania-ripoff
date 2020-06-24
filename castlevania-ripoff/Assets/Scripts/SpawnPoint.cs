using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject enemyToSpawn;
    public Transform whereToSpawn;
    public bool spawned;

    private void Awake()
    {
        spawned = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!spawned && collision.gameObject.CompareTag("Player"))
        {
            Instantiate(enemyToSpawn, whereToSpawn);
            spawned = true;
        }
    }

}
