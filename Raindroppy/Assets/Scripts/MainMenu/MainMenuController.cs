using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        GameObject TextHighScore = GameObject.FindWithTag("MainMenuHighScoreText");
        GameObject TextPoints = GameObject.FindWithTag("MainMenuPointsText");

        var textHighScore = TextHighScore.GetComponent<UnityEngine.UI.Text>();
        var textPoints = TextPoints.GetComponent<UnityEngine.UI.Text>();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            int highestScore = PlayerPrefs.GetInt("highestScore");
            textHighScore.text = highestScore.ToString();
        }
        else
        {
            textHighScore.text = "0";
        }

        if (PlayerPrefs.HasKey("coins"))
        {
            int points = PlayerPrefs.GetInt("coins");
            textPoints.text = points.ToString();
        }
        else
        {
            textPoints.text = "0";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void StartSkins()
    {
        SceneManager.LoadScene("SkinsScene");
    }

    public void StartChallenges()
    {
        SceneManager.LoadScene("ChallengesScene");
    }
}
