using UnityEngine;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject reconstructed;
    public GameObject scanned;
    public Text text;
    public static string modelMode;

    private bool showing = true;

    void Start()
    {
        // Ensure AT1600 is active and AT1600_SC is hidden initially
        reconstructed.SetActive(true);
        modelMode = "Reconstructed";

        text.text = "Scanned";
        scanned.SetActive(false);
    }

    public void SwitchModel()
    {
        showing = !showing;

        reconstructed.SetActive(showing);
        scanned.SetActive(!showing);
        if (reconstructed.activeSelf)
        {
            modelMode = "Reconstructed";
            text.text = "Scanned";
        } else
        {
            text.text = "Reconstructed";
            modelMode = "Scanned";
        }
    }
}
