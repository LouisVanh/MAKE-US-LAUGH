using System.Collections;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public float maxIntensity = 1.0f;
    public float minIntensity = 0.4f;
    public float flickerDurationMin = 0.02f;
    public float flickerDurationMax = 1f;
    public float flickerVariationMin = -0.2f;
    public float flickerVariationMax = 0.2f;

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            float t = 0.0f;
            float duration = Random.Range(flickerDurationMin, flickerDurationMax);
            float currIntensity = myLight.intensity;

            // Calculate targetIntensity with variation
            float targetIntensity = Mathf.Clamp(currIntensity + Random.Range(flickerVariationMin, flickerVariationMax), minIntensity, maxIntensity);

            while (t < duration)
            {
                myLight.intensity = Mathf.Lerp(currIntensity, targetIntensity, t / duration);
                t += Time.deltaTime;
                yield return null;
            }

            yield return null; // Add a small delay before starting the next iteration
        }
    }
}
