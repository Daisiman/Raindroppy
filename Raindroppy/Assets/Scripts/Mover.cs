using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float multiplier = 1;

    Rigidbody rb;
    GameController gameController;

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


        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * gameController.speed * multiplier;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * gameController.speed * multiplier;
    }
}
