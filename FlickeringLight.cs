using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 1.0f;

    private void Start()
    {
        // Check if a light component is assigned
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }

        // Start the flickering coroutine
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            // Randomly adjust the light intensity within the specified range
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);

            // Wait for a short duration before changing intensity again
            yield return new WaitForSeconds(1 / flickerSpeed);
        }
    }
}