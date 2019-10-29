using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public int lives = 1;
    public bool isBoss = true;
    GameController gameController;

    public int lifeValue;
    public bool addLives;
    public bool addScore;

    public bool powerUp;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (powerUp)
        {
            if (other.CompareTag("Player") && addLives)
            {
                gameController.AddLives(lifeValue);
            }

            if (other.CompareTag("Player") && addScore)
            {
                gameController.AddScore(scoreValue);
            }
        } else
        {
            if (other.CompareTag("Player"))
            {
                gameController.DecreaseLives();
            }
        }

        lives--;

        if (lives == 0) {
            Destroy(gameObject);

            if (!powerUp) {
                gameController.AddScore(scoreValue);
            }

            if (explosion != null) {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }

        if (other.CompareTag("Player") && gameController.lives == 0) {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
