using UnityEngine;

public class FollowCameraRotation : MonoBehaviour
{
    // Reference to the camera
    public Camera targetCamera;

    void Start()
    {
        // If no camera is assigned, use the main camera by default
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        transform.rotation = targetCamera.transform.rotation;
    }
}