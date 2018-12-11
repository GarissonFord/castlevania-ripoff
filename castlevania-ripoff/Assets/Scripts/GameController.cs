using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Canvas gameOverScreen;
    public Button restartButton, quitButton;

	// Use this for initialization
	void Start ()
    {
        gameOverScreen = gameOverScreen.GetComponent<Canvas>();
        restartButton = restartButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
        gameOverScreen.enabled = false;
    }
   
    public void GameOver()
    {
        //Create a menu to either quit or restart the level
        gameOverScreen.enabled = true;
        Debug.Log("GameOver entered");
    }

    public void RestartGame()
    {
        Debug.Log("Restart Clicked");
        //SceneManager.LoadScene("Test");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
    }
}
