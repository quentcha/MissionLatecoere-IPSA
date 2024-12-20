using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The amount of movement on the X axis.")]
    public float xSensitivity = 0.1f;

    [Tooltip("The amount of movement on the Y axis.")]
    public float ySensitivity = 0.1f;

    [Tooltip("The maximum movement range for the camera on the X axis.")]
    public float xMaxOffset = 1f;

    [Tooltip("The maximum movement range for the camera on the Y axis.")]
    public float yMaxOffset = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        // Get the mouse position in screen space
        Vector2 mousePosition = Input.mousePosition;

        // Normalize mouse position to a -1 to 1 range (center is 0,0)
        float normalizedX = (mousePosition.x / Screen.width - 0.5f) * 2;
        float normalizedY = (mousePosition.y / Screen.height - 0.5f) * 2;

        // Calculate the offset
        float xOffset = Mathf.Clamp(normalizedX * xSensitivity, -xMaxOffset, xMaxOffset);
        float yOffset = Mathf.Clamp(normalizedY * ySensitivity, -yMaxOffset, yMaxOffset);

        // Apply the offset to the initial position
        transform.position = new Vector3(
            initialPosition.x + xOffset,
            initialPosition.y + yOffset,
            initialPosition.z
        );
    }
}
