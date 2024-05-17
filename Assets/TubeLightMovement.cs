using UnityEngine;

public class TubeLightMovement : MonoBehaviour
{
    public float amplitude = 100f; // Adjust the amplitude of the oscillation
    public float frequency = 1f; // Adjust the frequency of the oscillation

    private float initialY; // Initial Y position

    void Start()
    {
        initialY = transform.position.y; // Store the initial Y position
    }

    void Update()
    {
        // Calculate the new Y position based on sine wave
        float newY = initialY + Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the tube light's position
        transform.position = new Vector3(transform.position.x, newY*5, transform.position.z);
    }
}
