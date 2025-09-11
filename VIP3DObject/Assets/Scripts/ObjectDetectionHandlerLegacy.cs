//using UnityEngine;
//using UnityEngine.UI;
//using Vuforia;

//public class ObjectDetectionHandlerLegacy : MonoBehaviour
//{
//    public Text objectNameText; // Legacy UI Text component

//    private ObserverBehaviour observerBehaviour;

//    void Start()
//    {
//        observerBehaviour = GetComponent<ObserverBehaviour>();
//        if (observerBehaviour)
//        {
//            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
//        }
//    }

//    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
//    {
//        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
//        {
//            Debug.Log("Detected object: " + behaviour.TargetName);
//            if (objectNameText != null)
//            {
//                objectNameText.text = "Detected: " + behaviour.TargetName;
//            }
//        }
//        else
//        {
//            if (objectNameText != null)
//            {
//                objectNameText.text = "No object detected";
//            }
//        }
//    }
//}





using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections.Generic;


public class ObjectDetectionHandlerLegacy : MonoBehaviour
{
    public Text objectNameText; // Legacy UI Text component
    public Text buttonText;
    public GameObject infoCanvas;
    public GameObject switchCanvas;
    public Dictionary<string, string> blockInfo = new Dictionary<string, string>();
    public Dictionary<string, string> blockName = new Dictionary<string, string>();
    public RawImage rawImage;
    public Texture texture;

    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        blockInfo["AT054_doorjamb"] = "<b>Description:</b> Wall jamb of the door of the cella.  Bears a three-fasciae carving at the end, that would have run around the door as a door frame. \n\n <b>Placement:</b> Found in the NW quadrant and thus belongs to the doorwall to the left of the entrance into the cella.";
        blockName["AT054_doorjamb"] = "AT054";

        blockInfo["AT290292_top_pilasterblock"] = "<b>Description:</b> These joining fragments form a pilaster block, which bears a necking in a similar manner to the upper anta block and the upper column shaft, and thus it is restored immediately below the pilaster capital.\n <b>Placement:</b> This fragments were found at the northeast  quadrant and they are restored at the rear left pilaster of the temple.";
        blockName["AT290292_top_pilasterblock"] = "AT290+292";

        blockInfo["AT1600"] = "<b>Description:</b> Rectalinear corinthian capital with three faces with bilateral symmetry, which identifies it as the anta capital. The capital bears a dowel hole.\n\n <b>Placement:</b> Found in the northwest quadrant, it is identified as the capital for the left anta.";
        blockName["AT1600"] = "AT160";

        blockInfo["AT429_SingleBlock"] = "<b>Description:</b> This block includes a cyma and a dentil course on one of its long faces, and thus is identified as a frieze block, over the architrave. However, it differs from other frieze blocks of the temple in that the dentils are at the edge of the block and thus are not crowned by a tainia, which would have made them more vulnerable to fractures during the installation and also would have reduced the overall height of the block.\n\n " +
            "<b>Placement:</b> This frieze block was found at the northeast of the temple. Given its specific findspot as well as other diagnostic elements, it was probably the first frieze block from the back corner on the north flank of the temple.";
        blockName["AT429_SingleBlock"] = "AT429";

        blockInfo["AT186"] = "<b>Description:</b> The frieze course succeeded the architrave, and was underneath the roof cornice (geison). Aesthetically, this course provided a surface for decoration, which in this case included a cyma profile and a dentil course. Functionally, this course at the back face included cuttings for the support of the  joists of the roof.\n\n" +
            "<b>Placement:</b> This is a corner frieze block, found to the southwest of the temple, and tbus is restored at the at the righthand corner of the temple front façade.";
        blockName["AT186"] = "AT186";

        blockInfo["AT638"] = "<b>Description:</b> Unfinished podium base molding. Its slanted side would be carved down at the final stages, to give a molding with an ovolo, cyma and tainia. The molding turns in a U-shape, thus the block has to be restored at the front end of the podium by the stairs. This is the only piece of evidence that gives us the width of the front end of the podium.\n\n" +
            "<b>Placement:</b> Was found about 20 m. southwest of the temple, and it is restored at the right front corner of the podium.";
        blockName["AT638"] = "AT638";

        switchCanvas.SetActive(false);
        infoCanvas.SetActive(false);
        //imagesCanvas.SetActive(false);
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            switchCanvas.SetActive(true);
            infoCanvas.SetActive(true);
            if (blockInfo.ContainsKey(behaviour.TargetName) && blockName.ContainsKey(behaviour.TargetName))
            {
                objectNameText.text = blockInfo[behaviour.TargetName];
                buttonText.text = blockName[behaviour.TargetName];
                rawImage.texture = texture;
            }


        }
        else
        {
            infoCanvas.SetActive(false);
            switchCanvas.SetActive(false);
            if (objectNameText != null)
            {
                objectNameText.text = "No object detected";
            }
        }
    }


}
