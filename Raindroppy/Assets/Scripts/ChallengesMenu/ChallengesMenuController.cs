using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengesMenuController : MonoBehaviour
{
    GameObject Challenge1Active;
    GameObject Challenge2Active;
    GameObject Challenge3Active;

    // Start is called before the first frame update
    void Start()
    {
        Challenge1Active = GameObject.FindWithTag("Challenge1Active");
        Challenge2Active = GameObject.FindWithTag("Challenge2Active");
        Challenge3Active = GameObject.FindWithTag("Challenge3Active");

        Challenge1Active.SetActive(PlayerPrefs.HasKey("challenge1"));
        Challenge2Active.SetActive(PlayerPrefs.HasKey("challenge2"));
        Challenge3Active.SetActive(PlayerPrefs.HasKey("challenge3"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
