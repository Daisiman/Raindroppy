using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int scoreValue;
    public bool fragile = false;
    public int lives = 1;
    GameController gameController;

    public int lifeValue;
    public bool goodDrops;
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
        // player hit
        if (other.CompareTag("Player"))
        {
            if (goodDrops)
            {
                gameController.AddLives(lifeValue);
            }
            else
            {
                gameController.DecreaseLives();
            }
            Destroy(gameObject);
        }
        // raindrop hits other drops or power-ups
        else if (other.CompareTag("Raindrop") && !gameObject.CompareTag("Obstacle"))
        {
            other.transform.localScale *= 1.1f;
            Destroy(gameObject);
        }
        // raindrop hits obstacle
        else if (other.CompareTag("Raindrop") && gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
        // two good drops collide
        else if (other.CompareTag("Lives") && gameObject.CompareTag("Lives"))
        {
            if (other.transform.localScale.x > gameObject.transform.localScale.x)
            {
                Destroy(other.gameObject);
                gameObject.transform.localScale *= 1.2f;
            }
            else
            {
                Destroy(gameObject);
                other.transform.localScale *= 1.2f;
            }
        }
    }
}
