using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public Canvas gameOverScreen, quitMenu;
    public Button restartButton, quitButton, yesButton, noButton;
    public Text gameOverText, quitText;

    // Use this for initialization
    void Start()
    {
        gameOverScreen = gameOverScreen.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        restartButton = restartButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
        yesButton = yesButton.GetComponent<Button>();
        noButton = noButton.GetComponent<Button>();
        gameOverScreen.enabled = false;
        quitMenu.enabled = false;
        gameOverText.enabled = false;
        quitText.enabled = false;
    }

    public void GameOver()
    {
        //Create a menu to either quit or restart the level
        gameOverScreen.enabled = true;
        gameOverText.enabled = true;
        restartButton.enabled = true;
        quitButton.enabled = true;
        quitMenu.enabled = false;
        quitText.enabled = false;
    }

    public void RestartGame()
    {
        Debug.Log("Restart Clicked");
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        restartButton.enabled = false;
        quitButton.enabled = false;
        gameOverText.enabled = false;
        quitMenu.enabled = true;
        quitText.enabled = true;
        Debug.Log("Quit Clicked");
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        GameOver();
    }
}
