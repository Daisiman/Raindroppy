using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        Debug.Log("Destroy by time " + gameObject);
        Destroy(gameObject, lifetime);
    }
}
