using UnityEngine;

public class ConsumePowerUp : MonoBehaviour
{
    public bool isLivesPowerUp;
    public bool isBoostPowerUp;
    AudioSource boostSound;

    GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        boostSound = GetComponent<AudioSource>();
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
                if (boostSound != null)
                {
                    boostSound.PlayOneShot(boostSound.clip);
                }
            }

            Destroy(gameObject);
        }
    }
}

