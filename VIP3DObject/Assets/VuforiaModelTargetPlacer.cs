using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaModelTracker : MonoBehaviour
{
    public string targetName; // The name of the model target
    public GameObject modelPrefab; // Correct prefab for this target
    public Camera arCamera;
    private GameObject spawnedObject;
    private GameObject selectedObject = null;
    private Vector2 lastTouchPosition;
    private float rotationSpeed = 150f;

    private ModelTargetBehaviour modelTargetBehaviour;

    void Start()
    {
        modelTargetBehaviour = GetComponent<ModelTargetBehaviour>();
        if (modelTargetBehaviour != null)
        {
            modelTargetBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;
        }
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED)
        {
            if (behaviour.TargetName == targetName)
            {
                Debug.Log($"Detected Model: {targetName}");
                PlaceModel();
            }
        }
        else
        {
            if (spawnedObject != null)
            {
                spawnedObject.SetActive(false);
            }
        }
    }

    private void PlaceModel()
    {
        if (modelPrefab != null && arCamera != null)
        {
            if (spawnedObject == null) // Instantiate only if not already done
            {
                spawnedObject = Instantiate(modelPrefab);
                spawnedObject.transform.position = arCamera.transform.position + arCamera.transform.forward * 0.9f;
                spawnedObject.transform.rotation = arCamera.transform.rotation;
                spawnedObject.transform.localScale = Vector3.one * 0.1f;

                // Ensure collider exists for touch handling
                if (spawnedObject.GetComponent<Collider>() == null)
                {
                    spawnedObject.AddComponent<BoxCollider>();
                }

                // Optionally hide the child at the start
                Transform child = spawnedObject.transform.GetChild(0);
                child.gameObject.SetActive(false);
            }

            spawnedObject.SetActive(true);
        }
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.gameObject == spawnedObject)
                    {
                        selectedObject = spawnedObject;
                        lastTouchPosition = touch.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && selectedObject != null)
            {
                Vector2 touchDelta = touch.position - lastTouchPosition;
                float rotationAmount = touchDelta.x * rotationSpeed * Time.deltaTime;

                // Rotate around Y-axis
                selectedObject.transform.Rotate(Vector3.up, -rotationAmount, Space.World);

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                selectedObject = null;
            }
        }
    }

    public void switchP()
    {
        if (spawnedObject != null)
        {
            Transform child = spawnedObject.transform.GetChild(0);
            Renderer[] parentRenderers = spawnedObject.GetComponentsInChildren<Renderer>();
            Renderer[] childRenderers = child.GetComponentsInChildren<Renderer>();

            bool isChildActive = child.gameObject.activeSelf;

            if (isChildActive)
            {
                // Hide child and show parent
                foreach (var renderer in childRenderers) renderer.enabled = false;
                child.gameObject.SetActive(false);
                foreach (var renderer in parentRenderers) renderer.enabled = true;
            }
            else
            {
                // Show child and hide parent
                foreach (var renderer in parentRenderers) renderer.enabled = false;
                child.gameObject.SetActive(true);
                foreach (var renderer in childRenderers) renderer.enabled = true;
            }
        }
    }

    void OnDestroy()
    {
        if (modelTargetBehaviour != null)
        {
            modelTargetBehaviour.OnTargetStatusChanged -= OnTrackableStateChanged;
        }
    }
}
