using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 1.0f;
    public float flickerRandomness = 0.1f;
    float random;

    void Start()
    {
        random = Random.Range(0.0f, 65535.0f) + flickerRandomness;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * flickerSpeed);
        flickeringLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}

