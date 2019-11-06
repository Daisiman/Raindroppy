using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    GameController gameController;
    public float tileSizeZ;

    private Vector3 startPosition;

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

        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float offsetAddition = gameController.speed * Time.fixedDeltaTime;
        float existingOffset = transform.position.z;
        float newPosition = Mathf.Repeat(existingOffset + offsetAddition, tileSizeZ);

        transform.position = startPosition + Vector3.forward * newPosition;
    }
}