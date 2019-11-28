using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public bool paused = false;
    GameController gameController;
    GameObject MainMenuButton;
    GameObject PauseMenuButton;

    public Sprite PauseImg;
    public Sprite PlayImg;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton = GameObject.FindWithTag("Button");
        PauseMenuButton = GameObject.FindWithTag("Pause");
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
        PauseMenuButton.GetComponent<UnityEngine.UI.Image>().sprite = gameController.paused ? PauseImg : PlayImg;

        gameController.PauseGame();

        MainMenuButton.SetActive(gameController.paused);
    }
}
