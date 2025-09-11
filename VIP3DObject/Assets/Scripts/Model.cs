using UnityEngine;

public class Model : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private bool isDragging = false;
    public float rotationSpeed = 0.2f;
    public int orientation = 0;

    void Update()
    {
        if (Input.touchCount > 0)
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


                        // Rotate smoothly in local space
                        if (orientation == 1)
                        {
                            transform.Rotate(rotateX, 0, 0, Space.Self);
                            
                        } else if(orientation == 2)
                        {
                            transform.Rotate(0, 0, rotateY, Space.Self);
                        }
                        else
                        {
                            transform.Rotate(0, rotateY, 0, Space.Self);
                        }
                        startTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}