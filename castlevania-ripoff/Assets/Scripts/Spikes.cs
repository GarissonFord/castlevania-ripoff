using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public PlayerController pc;
    public Healthbar healthbar;
    public GameController gameController;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        healthbar = FindObjectOfType<Healthbar>();
        gameController = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.GameOver();
            pc.hitPoint = 0;
            pc.UpdateHealthbar();
            pc.SendMessageUpwards("Die");
        }
    }
}
