using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public Canvas MainMenu;
    public Button startButton, quitButton;

	// Use this for initialization
	void Start ()
    {
        MainMenu = MainMenu.GetComponent<Canvas>();
        startButton = startButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
	}
	
	public void StartGame()
    {
        Debug.Log("Start Game clicked");
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game clicked");
    }
}
