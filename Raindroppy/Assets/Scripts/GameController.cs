using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject boss;
    public GameObject[] hazards;
    public GameObject[] raindrops;
    public GameObject[] powerUps;
    public Vector3 spawnValues;
    public Vector3 raindropSpawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int speed;
    private bool isImmortal = false;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText livesText;

    public int cycleLength;

    public bool gameOver {
        private set;
        get;
    }

    bool restart;
    int score;

    public int lives;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        gameOverText.text = "";
        score = 0;
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
	}

	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int j = 0; j < cycleLength; j++) {
                {
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

                    //// raindrop
                    //GameObject raindrop = raindrops[Random.Range(0, raindrops.Length)];
                    //Vector3 spawnPosition = new Vector3(Random.Range(-raindropSpawnValues.x, raindropSpawnValues.x), raindropSpawnValues.y, raindropSpawnValues.z);
                    //Quaternion spawnRotation = Quaternion.identity;
                    //Instantiate(raindrop, spawnPosition, spawnRotation);
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
        score += newScoreValue;
        UpdateScore();
    }

    public void DoubleLives()
    {
        lives *= 2;
        UpdateLives();
    }

    public void GiveBoost()
    {
        speed = -5;
        isImmortal = true;
    }

    public void AddLives(int lifeValue)
    {
        lives += lifeValue;
        UpdateLives();
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
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

        if (PlayerPrefs.HasKey("highestScore"))
        {
            int highestScore = PlayerPrefs.GetInt("highestScore");

            gameOverText.text += " Highscore: " + highestScore;

            if (score > highestScore) {
                gameOverText.text = "New highscore!";

                PlayerPrefs.SetInt("highestScore", score);
            }
        } else {
            gameOverText.text = "New highscore!";
            PlayerPrefs.SetInt("highestScore", score);
        }
    } 
}
