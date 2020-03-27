using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    /* The quitMenu is a separate Canvas that appears when you decide to select
     * 'quit' button in the game over Canvas
     */ 
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
        /* In case we go back to the GameOver menu after clicking on 'No'
         * when the quitMenu asks "Are you sure you want to quit?"
         */ 
        quitMenu.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        //Asks "Are you sure you want to quit?"
        quitMenu.enabled = true;
        gameOverMenu.enabled = false;
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        //Brings us back to the first game over menu
        GameOver();
    }
}
