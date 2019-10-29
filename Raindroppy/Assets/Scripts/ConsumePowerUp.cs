using UnityEngine;

public class ConsumePowerUp : MonoBehaviour
{
    public int scoreValue;
    public int lifeValue;
    public bool addLives;
    public bool addScore;
    GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (addLives)
        {
            gameController.AddLives(lifeValue);
        }

        if (addScore) {
            gameController.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}

