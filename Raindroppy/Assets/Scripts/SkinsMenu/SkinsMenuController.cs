using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinsMenuController : MonoBehaviour
{
    GameObject TextPoints;
    GameObject TextPrice;
    GameObject Skin1Btn;
    GameObject Skin2Btn;
    GameObject Skin1Active;
    GameObject Skin2Active;

    // Start is called before the first frame update
    void Start()
    {
        TextPoints = GameObject.FindWithTag("MainMenuPointsText");
        TextPrice = GameObject.FindWithTag("PriceText");
        Skin1Btn = GameObject.FindWithTag("Skin1Btn");
        Skin2Btn = GameObject.FindWithTag("Skin2Btn");
        Skin1Active = GameObject.FindWithTag("Skin1Active");
        Skin2Active = GameObject.FindWithTag("Skin2Active");
        init();
    }

    void init() 
    {
        var textPoints = TextPoints.GetComponent<UnityEngine.UI.Text>();
        var textPrice = TextPrice.GetComponent<UnityEngine.UI.Text>();
        var skin1Btn = Skin1Btn.GetComponent<UnityEngine.UI.Button>();
        var skin2Btn = Skin2Btn.GetComponent<UnityEngine.UI.Button>();

        if (PlayerPrefs.HasKey("coins"))
        {
            int points = PlayerPrefs.GetInt("coins");
            textPoints.text = points.ToString();
        }
        else
        {
            textPoints.text = "0";
        }

        int hasSkin = 0;

        if (PlayerPrefs.HasKey("skin2"))
        {
            hasSkin = PlayerPrefs.GetInt("skin2");
        }

        if (hasSkin == 1)
        {
            textPrice.text = "";
        }

        int coins = 0;
        if (PlayerPrefs.HasKey("coins"))
        {
            coins = PlayerPrefs.GetInt("coins");
        }

        int skin = 1;
        if (PlayerPrefs.HasKey("skin"))
        {
            skin = PlayerPrefs.GetInt("skin");
        }

        if (skin == 1)
        {
            skin1Btn.interactable = false;
            skin2Btn.interactable = true;
            Skin1Active.SetActive(true);
            Skin2Active.SetActive(false);
        }

        if (skin == 2)
        {
            skin1Btn.interactable = true;
            skin2Btn.interactable = false;
            Skin1Active.SetActive(false);
            Skin2Active.SetActive(true);
        }

        if (hasSkin == 0 & coins < 10) {
            skin2Btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void selectSkin2() {
        int hasSkin = 0;

        if (PlayerPrefs.HasKey("skin2")) {
            hasSkin = PlayerPrefs.GetInt("skin2");
        }

        if (hasSkin == 1) {
            PlayerPrefs.SetInt("skin", 2);
            init();
            return;
        }

        int coins = 0;
        if (PlayerPrefs.HasKey("coins"))
        {
            coins = PlayerPrefs.GetInt("coins");
        }

        coins -= 10;
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("skin", 2);
        PlayerPrefs.SetInt("skin2", 1);
        init();
    }

    public void selectSkin1()
    {
        PlayerPrefs.SetInt("skin", 1);
        init();
    }
}
