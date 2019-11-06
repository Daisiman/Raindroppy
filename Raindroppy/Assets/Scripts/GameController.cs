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
    float score;

    public int lives;
    private float livesHelper;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        gameOverText.text = "";
        score = 0f;
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
            score += speed/20*(-1);
            UpdateScore();

            if (!isImmortal)
            {
                livesHelper += speed / 100 * (-1);

                if(livesHelper > 10f)
                {
                    DecreaseLives();
                    UpdateLives();
                    livesHelper = 0;
                }
            }

            player.transform.localScale = new Vector3(1f, 1f, 1f) * Mathf.Log10(lives);
            speed = -1 * Mathf.Log10(lives);
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
            speed -= 0.1f;
            yield return new WaitForSeconds(i / 10);
        }

        yield return new WaitForSeconds(3f);

        for (float i = 0f; i < 1f; i += 0.01f)
        {
            speed += 0.1f;
            yield return new WaitForSeconds(i / 10);
        }

        isImmortal = false;
    }

    public void AddLives(int lifeValue)
    {
        lives += lifeValue;
        UpdateLives();
    }

    void UpdateScore()
    {
        int scoreValue = (int)Mathf.Round(score);
        scoreText.text = $"Score: {scoreValue}";
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
                gameOverText.text = "New highscore!";

                PlayerPrefs.SetInt("highestScore", (int)Mathf.Round(score));
            }
        } else {
            gameOverText.text = "New highscore!";
            PlayerPrefs.SetInt("highestScore", (int)Mathf.Round(score));
        }
    }
}
