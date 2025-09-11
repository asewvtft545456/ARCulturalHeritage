using UnityEngine;
using Vuforia;

public class PositionModel : MonoBehaviour
{
    public GameObject modelPrefab;
    private GameObject modelInstance;
    private bool isModelVisible = false;

    void Update()
    {
        // Get the ModelTargetBehaviour component
        ModelTargetBehaviour modelTargetBehaviour = GetComponent<ModelTargetBehaviour>();

        if (modelTargetBehaviour != null)
        {
            if (modelTargetBehaviour.TargetStatus.Status == Status.TRACKED && !isModelVisible)
            {
                // Get the target size
                Vector3 targetSize = modelTargetBehaviour.GetSize();
                float targetWidth = targetSize.x;

                Debug.Log("Target Width: " + targetWidth);

                // Instantiate the model prefab only once
                if (modelInstance == null)
                {
                    modelInstance = Instantiate(modelPrefab, transform.position, transform.rotation, transform);

                    // Scale the model based on the target's size
                    modelInstance.transform.localScale = new Vector3(targetWidth, targetWidth, targetWidth) * 0.5f;

                    // Position the model directly on the target
                    modelInstance.transform.localPosition = new Vector3(0, 0, 0.1f);
                    modelInstance.transform.localRotation = Quaternion.identity;

                    Debug.Log("Model instantiated and scaled");
                }

                isModelVisible = true;
            }
            else if (modelTargetBehaviour.TargetStatus.Status != Status.TRACKED && isModelVisible)
            {
                // Hide the model when tracking is lost
                if (modelInstance != null)
                {
                    modelInstance.SetActive(false);
                }

                isModelVisible = false;
            }
        }
    }
}

