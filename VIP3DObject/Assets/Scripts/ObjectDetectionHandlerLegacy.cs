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

        blockInfo["AT308"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT308"] = "AT308";

        blockInfo["AT145"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT145"] = "AT145";

        blockInfo["AT146"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT146"] = "AT146";

        blockInfo["AT175"] = "<b>Description:</b> Fragment of the top column drum of the shaft of the column. It is not known if the column consisted of 2 or 3 column drums. In either case, the columns were unfluted, and were made of the same marble as throughout the temple, and had entasis and a smaller diameter at the upper part. This frament preserves the upper resting surface of the column, where the column capital would be supported. It bears a dowel hole, for the alignment and fastening of the capital.\n\n" +
            "<b>Placement:</b> This column drum was found to the southeast of the temple and is restored at the rightmost column.";
        blockName["AT175"] = "AT175";

        blockInfo["AT173"] = "<b>Description:</b> Column capital, the best preserved of the four original ones. The capital is of the corinthian genus, bearing stylized acanthus leaves and tendrils in the corners and the middle part of the capital. The corinthian order was commonly used in Imperial Roman Architecture, as the most ornate order. \n\n" +
            "<b>Placement:</b> Found in the SW quadrant.";
        blockName["AT173"] = "AT173";

        blockInfo["AT085"] = "<b>Description:</b> This base bears the scotia-torus-scotia molding in three sides with bilateral symmetry and thus is identified as the anta base, meaning the base to the buldging extension of one of the two lateral walls of the cella.\n\n" +
            "<b>Placement:</b> Anta base found in NW quadrant, and restored on the left side of the temple.";
        blockName["AT085"] = "AT085";

        blockInfo["AT159"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT159"] = "AT159";

        //Combine 091 and 051
        blockInfo["AT091"] = "<b>Description:</b> Right side of the door lintel of the entrance to the cella. It continues the three-fasciate door frame at the top, which is crowned by a lintel, framed by a scroll in either end.\n\n" +
            "<b>Placement:</b> It was found in the northwest quadrant along with the other corner of the lintel.";
        blockName["AT091"] = "AT091";

        blockInfo["AT051"] = "<b>Description:</b> Left side of the door lintel of the entrance to the cella. It continues the three-fasciate door frame at the top, which is crowned by a lintel, framed by a scroll in either end.\n\n" +
            "<b>Placement:</b> It was found in the northwest quadrant along with the other corner of the lintel.";
        blockName["AT051"] = "AT051";
        //Combine 091 and 051

        blockInfo["AT188"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT188"] = "AT188";

        blockInfo["AT421"] = "<b>Description:</b> This is a corner block of the architrave. It includes three fasciae of increasing height crowned by a simple ovolo and cavetto. It's short length shows that it could not have spanned the opening between columns, and has to be restored in the cella, over the wall blocks. \n\n" +
            "<b>Placement:</b> It was found at the northeast of the temple, and it is restored at the rear left corner of temple.";
        blockName["AT421"] = "AT421";

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
