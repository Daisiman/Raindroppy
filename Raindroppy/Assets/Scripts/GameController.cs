using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] raindrops;
    public GameObject[] powerUps;
    public Vector3 spawnValues;
    public Vector3 raindropSpawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float speed;
    public float speedBoost;
    private bool isImmortal;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText livesText;
    public GUIText coinsText;
    public GUIText totalCoinsText;

    public int cycleLength;

    public bool gameOver {
        private set;
        get;
    }

    bool restart;
    float score;
    int coins;

    public int lives;
    private float livesHelper;

    GameObject player;
    PlayerController playerController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        totalCoinsText.text = "";
        coinsText.text = "";
        score = 0f;
        coins = 0;
        UpdateScore();
        UpdateLives();
        StartCoroutine(SpawnWaves());
    }

	void Update()
	{
        if (restart) {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            score += speed/50*(-1);
            UpdateScore();
            UpdateCoins();

            if (!isImmortal)
            {
                livesHelper += speed / 30 * (-1);

                if(livesHelper > 10f)
                {
                    DecreaseLives();
                    UpdateLives();
                    livesHelper = 0;
                }
            }
            if (!gameOver) {
                player.transform.localScale = Vector3.MoveTowards(player.transform.localScale, new Vector3(1f, 1f, 1f) * Mathf.Max(Mathf.Log10(lives), 0.5f), Time.deltaTime * 3);
            }
            speed = speedBoost + -1 * Mathf.Max(Mathf.Log(lives, 2), 0.5f);
        }
	}

	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int j = 0; j < cycleLength; j++) {
                {
                    // raindrop
                    GameObject raindrop = raindrops[Random.Range(0, raindrops.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-raindropSpawnValues.x, raindropSpawnValues.x), raindropSpawnValues.y, raindropSpawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(raindrop, spawnPosition, spawnRotation);
                }


                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }

                if (j == cycleLength - 1)
                {

                    Debug.Log("Spawn powerup");
                    // power up
                    GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
                    Vector3 spawnPositionPowerUp = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotationPowerUp = Quaternion.identity;
                    Instantiate(powerUp, spawnPositionPowerUp, spawnRotationPowerUp);

                    yield return new WaitForSeconds(spawnWait);
                }

                yield return new WaitForSeconds(waveWait);

                if (gameOver) {
                    break;
                }
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        //score += (float)newScoreValue;
        UpdateScore();
    }

    public void DoubleLives()
    {
        lives *= 2;
        UpdateLives();
    }


    public void GiveBoost()
    {
        StartCoroutine(GiveBoostCoroutine());
    }

    IEnumerator GiveBoostCoroutine()
    {
        isImmortal = true;

        for (float i = 0f; i < 1f; i += 0.01f)
        {
            speedBoost -= 0.1f;
            yield return new WaitForSeconds(i / 30);
        }

        yield return new WaitForSeconds(3f);

        for (float i = 0f; i < 1f; i += 0.01f)
        {
            speedBoost += 0.1f;
            yield return new WaitForSeconds(i / 30);
        }

        isImmortal = false;
    }

    public void AddLives(int lifeValue)
    {
        lives += lifeValue;
        playerController.PlayPickupSound();
        UpdateLives();
    }

    public void AddCoins(int coinValue)
    {
        coins += coinValue;
        UpdateCoins();
    }

    void UpdateScore()
    {
        int scoreValue = (int)Mathf.Round(score);
        scoreText.text = $"Score: {scoreValue}";
    }

    void UpdateCoins()
    {
        coinsText.text = $"Droplets: {coins}";
    }

    void UpdateLives()
    {
        livesText.text = $"Lives: {lives}";
    }

    public void DecreaseLives() {
        if (!isImmortal)
        {
            if (lives > 0)
            {
                lives--;
            }

            UpdateLives();

            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
        speed = 0;
        Destroy(GameObject.FindWithTag("Player"));

        if (PlayerPrefs.HasKey("highestScore"))
        {
            int highestScore = PlayerPrefs.GetInt("highestScore");

            gameOverText.text += " Highscore: " + highestScore;

            if (score > highestScore) {
                gameOverText.text = "New Highscore!";

                PlayerPrefs.SetInt("highestScore", (int)Mathf.Round(score));
            }
        } else {
            gameOverText.text = "New Highscore!";
            PlayerPrefs.SetInt("highestScore", (int)Mathf.Round(score));
        }

        int totalCoins = 0;

        if (PlayerPrefs.HasKey("coins"))
        {
            totalCoins = PlayerPrefs.GetInt("coins");
        }

        totalCoins += coins;
        PlayerPrefs.SetInt("coins", totalCoins);

        totalCoinsText.text = "Total Droplets: " + totalCoins;
    }
}
