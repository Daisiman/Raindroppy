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

                if (gameController.lives == 0)
                {
                    //Destroy(other.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }
}
