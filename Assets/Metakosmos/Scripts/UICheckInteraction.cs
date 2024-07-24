using System.Collections.Generic;
using UnityEngine;


public class UICheckInteraction : MonoBehaviour
{
    public List<GameObject> gameObjectsToCheck; 
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor xrRayInteractorRight; 
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor xrRayInteractorLeft; 

    private void Update()
    {
        bool anyActive = false;

        foreach (GameObject obj in gameObjectsToCheck)
        {
            if (obj.activeSelf)
            {
                anyActive = true;
                break;
            }
        }

        xrRayInteractorRight.enabled = !anyActive;
        xrRayInteractorLeft.enabled = !anyActive;
    }
}
