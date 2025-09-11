using UnityEngine;

public class CustomRotate : MonoBehaviour
{
    private float rotationSpeed = 5f; // Adjust for sensitivity

    void Update()
    {
        if (Input.touchCount > 0) // Check if there is a touch
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationY = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime; // Left/Right swipe rotates on Y-axis

                transform.Rotate(0, 0, rotationY, Space.Self); // Rotate around the model's Y-axis
            }
        }
    }
}
