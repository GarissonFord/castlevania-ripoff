using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject enemy;

    public float timeBetweenSpawns, timeOfLastSpawn;

    private void Start()
    {
        Instantiate(enemy);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time - timeOfLastSpawn >= timeBetweenSpawns)
            Instantiate(enemy);
	}
}
