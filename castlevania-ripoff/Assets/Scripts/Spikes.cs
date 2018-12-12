using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spikes : MonoBehaviour {

    public PlayerController pc;
    public Healthbar healthbar;
    public GameOverMenuScript goms;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        healthbar = FindObjectOfType<Healthbar>();
        goms = FindObjectOfType<GameOverMenuScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goms.GameOver();
            pc.hitPoint = 0;
            pc.UpdateHealthbar();
            pc.SendMessageUpwards("Die");
        }
    }
}
