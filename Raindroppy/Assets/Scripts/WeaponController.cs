using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    public int mode;

    public int series;
    public float seriesDelay;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (mode == 0)
        {
            InvokeRepeating("Fire", delay, fireRate);
        }
        else
        {
            StartCoroutine(FireSeries());
        }
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

    IEnumerator FireSeries()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            for (int i = 0; i < series; i++)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
                yield return new WaitForSeconds(fireRate);
            }

            yield return new WaitForSeconds(seriesDelay);
        }
    }
}
