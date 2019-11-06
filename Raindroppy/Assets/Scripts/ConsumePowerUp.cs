using UnityEngine;

public class ConsumePowerUp : MonoBehaviour
{
    public bool isLivesPowerUp;
    public bool isBoostPowerUp;
    public AudioSource boostSound;

    GameController gameController;
    SoundController soundController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        GameObject soundControllerObject = GameObject.FindWithTag("SoundController");
        if (soundControllerObject != null)
        {
            soundController = soundControllerObject.GetComponent<SoundController>();
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
                if (soundController != null)
                {
                    soundController.PlayBoost();
                }
            }

            Destroy(gameObject);
        }
    }
}

