using UnityEngine;

public class ConsumePowerUp : MonoBehaviour
{
    public int scoreValue;
    public bool isLivesPowerUp;
    public bool isBoostPowerUp;
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
        if (other.CompareTag("Player"))
        {
            if (isLivesPowerUp)
            {
                gameController.DoubleLives();
            }

            if (isBoostPowerUp)
            {
                gameController.GiveBoost();
            }

            Destroy(gameObject);
        }
    }
}

