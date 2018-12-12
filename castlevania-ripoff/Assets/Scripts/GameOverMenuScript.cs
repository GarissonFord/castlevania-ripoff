using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public Canvas gameOverMenu, quitMenu;

    // Use this for initialization
    void Start()
    {
        quitMenu.enabled = false;
        gameOverMenu.enabled = false;
    }

    public void GameOver()
    {
        //Create a menu to either quit or restart the level
        gameOverMenu.enabled = true;
        quitMenu.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        quitMenu.enabled = true;
        gameOverMenu.enabled = false;
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
