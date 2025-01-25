using UnityEngine;

public class FluctuatingScale : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale factor
    public float maxScale = 2.0f; // Maximum scale factor
    public float speed = 1.0f;    // Speed of fluctuation

    private Vector3 originalScale;

    void Start()
    {
        // Save the original scale of the object
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the fluctuation factor using Mathf.PingPong to oscillate
        float fluctuation = Mathf.PingPong(Time.time * speed, 1f);

        // Interpolate between the min and max scale based on the fluctuation
        float scale = Mathf.Lerp(minScale, maxScale, fluctuation);

        // Apply the fluctuating scale to the object's local scale
        transform.localScale = originalScale * scale;
    }
}