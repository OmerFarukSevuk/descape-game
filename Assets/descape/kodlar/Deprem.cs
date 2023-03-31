using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deprem : MonoBehaviour
{
    public timer timer;
    public float maxMagnitude = 0.5f;  // Maximum earthquake magnitude
    public float duration = 60.0f;     // Duration of earthquake (in seconds)
    public float magnitudeIncreasePerSecond = 0.01f; // Magnitude increase per second

    public Transform[] objectsToShake;  // List of objects to shake
    public float objectFallThreshold = 0.5f;  // Object fall threshold

    private Vector3 originalPosition;  // Original position of the house
    private float elapsedTime = 0.0f;  // Elapsed time
    private float currentMagnitude = 0.0f; // Current earthquake magnitude

    void Start()
    {
        originalPosition = transform.position;  // Save the original position of the house
    }

    void Update()
    {
        if (timer.timeRemaining<30)
        {
            Shake();
        }
    }

    void Shake()
    {
        elapsedTime = 0.0f;  // Reset elapsed time
        currentMagnitude = 0.0f; // Reset current earthquake magnitude

        // Save the original positions of the objects to shake
        Vector3[] originalObjectPositions = new Vector3[objectsToShake.Length];
        for (int i = 0; i < objectsToShake.Length; i++)
        {
            originalObjectPositions[i] = objectsToShake[i].position;
        }

        // Start shaking routine
        StartCoroutine(ShakeRoutine(originalObjectPositions));
    }

    IEnumerator ShakeRoutine(Vector3[] originalObjectPositions)
    {
        while (elapsedTime < duration)  // Shake for the specified duration
        {
            currentMagnitude = Mathf.Min(currentMagnitude + magnitudeIncreasePerSecond * Time.deltaTime, maxMagnitude);

            // Shake the house position
            float x = originalPosition.x + Random.Range(-currentMagnitude, currentMagnitude);
            float y = originalPosition.y + Random.Range(-currentMagnitude, currentMagnitude);
            float z = originalPosition.z + Random.Range(-currentMagnitude, currentMagnitude);
            transform.position = new Vector3(x, y, z);

            // Shake the objects
            for (int i = 0; i < objectsToShake.Length; i++)
            {
                Vector3 objectPosition = originalObjectPositions[i];
                x = objectPosition.x + Random.Range(-currentMagnitude, currentMagnitude);
                y = objectPosition.y + Random.Range(-currentMagnitude, currentMagnitude);
                z = objectPosition.z + Random.Range(-currentMagnitude, currentMagnitude);
                objectsToShake[i].position = new Vector3(x, y, z);

                // Destroy the object if it falls below the fall threshold
                if (objectsToShake[i].position.y < objectFallThreshold)
                {
                    Destroy(objectsToShake[i].gameObject);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the positions of the objects to their original positions
        for (int i = 0; i < objectsToShake.Length; i++)
        {
            objectsToShake[i].position = originalObjectPositions[i];
        }
        transform.position = originalPosition;
    }
}
