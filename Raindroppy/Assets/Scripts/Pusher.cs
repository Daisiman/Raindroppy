using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody body = collision.rigidbody;
        Debug.Log("Push");
        if (body != null && !body.isKinematic)
        {
            body.AddForce(transform.forward);
            body.velocity += collision.impulse * 0.01f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Push 2");
    }
}
