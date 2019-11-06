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
        } else if (other.CompareTag("Raindrop"))
        {
            Destroy(gameObject);
        }
    }
}
