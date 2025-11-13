using UnityEngine;

public class Model : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private bool isDragging = false;
    public float rotationSpeed = 0.2f;
    public int orientation = 0;
    public float verticalClampAngle = 20f;
    private Quaternion initialRotation;
    private string previousModelMode = "";
    public float zoomSpeed = 0.001f;

    void Start()
    {
        initialRotation = transform.localRotation;
        previousModelMode = ModelSwitcher.modelMode;
    }

    void Update()
    {

        if (ModelSwitcher.modelMode != previousModelMode)
        {
            ResetModelRotation();
            previousModelMode = ModelSwitcher.modelMode;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 currentTouchPosition = touch.position;
                        Vector2 delta = currentTouchPosition - startTouchPosition;

                        float rotateX = delta.y * rotationSpeed;
                        float rotateY = -delta.x * rotationSpeed;

                        if (ModelSwitcher.modelMode == "Reconstructed")
                        {
                            transform.Rotate(rotateX, 0, 0, Space.Self);
                            transform.Rotate(0, rotateY, 0, Space.World);
                        }
                        else
                        {
                            transform.Rotate(0, rotateY, 0, Space.World);
                            Vector3 currentRotation = transform.localEulerAngles;
                            float desiredX = currentRotation.x - rotateX;
                            if (desiredX > 180f) desiredX -= 360f;
                            desiredX = Mathf.Clamp(desiredX, -verticalClampAngle, verticalClampAngle);
                            transform.localEulerAngles = new Vector3(desiredX, currentRotation.y, currentRotation.z);
                        }

                        startTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition; 

            float prevMagnitude = (touch0Prev - touch1Prev).magnitude; 
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Vector3 newScale = transform.localScale + Vector3.one * difference * zoomSpeed;
            newScale = Vector3.Max(newScale, Vector3.one * 0.3f);
            newScale = Vector3.Min(newScale, Vector3.one * 3f);
            transform.localScale = newScale;

        }
    }

    private void ResetModelRotation()
    {
        // Reset to initial rotation
        transform.localRotation = initialRotation;
    }

}