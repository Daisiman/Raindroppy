using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public bool paused = false;
    GameController gameController;
    GameObject MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton = GameObject.FindWithTag("Button");
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        MainMenuButton.SetActive(gameController.paused);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        gameController.PauseGame();
    }

    public void PauseMenu()
    {
        gameController.PauseGame();

        MainMenuButton.SetActive(gameController.paused);
    }
}
