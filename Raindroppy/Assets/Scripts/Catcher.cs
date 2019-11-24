using UnityEngine;
using System.Collections;

public class Catcher : MonoBehaviour
{
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    Transform playerTransfrom;
    GameController gameController;

    float currentSpeed;
    float targetManeuver;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        else if (gameController.lives != 0 && GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentSpeed = rb.velocity.z;
        StartCoroutine(Catch());
    }

    IEnumerator Catch()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            if (gameController != null && gameController.lives != 0 && playerTransfrom.position.z > rb.position.z)
            {
                targetManeuver = playerTransfrom.position.x;
            }
      
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
    }
}