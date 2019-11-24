using UnityEngine;

public class Shrinker : MonoBehaviour
{
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    public float rate = 1;

    bool shrink = true;

    void FixedUpdate()
    {
        if (shrink)
        {
            if (transform.localScale.x > minScale.x &&
                transform.localScale.y > minScale.y &&
                transform.localScale.z > minScale.z)
            {
                transform.localScale -= rate * new Vector3(0.001f, 0.001f, 0.001f);
            }
            else
            {
                shrink = false;
            }
        }
    }
}
