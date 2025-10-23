using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ButtonControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Text objectNameText; // Legacy UI Text component
    public GameObject infoCanvas;
    public GameObject displayer;
    public GameObject tab;
    
    void Start()
    {
        
        showJustText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showJustText()
    {
        tab.SetActive(true);
        objectNameText.gameObject.SetActive(true);
        displayer.SetActive(false);
        //rawImage.texture = null;
       
    }

    public void showJustImages()
    {
        tab.SetActive(false);
        objectNameText.gameObject.SetActive(false);
        displayer.SetActive(true);
        
    }
}
