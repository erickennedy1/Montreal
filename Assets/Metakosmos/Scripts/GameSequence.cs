using System.Collections.Generic;
using UnityEngine;


public class GameSequence : MonoBehaviour
{
public List<GameObject> objectsToActivate;

    void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            Outline outline = obj.GetComponent<Outline>();
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

            if (outline != null)
                outline.enabled = false;

            if (interactable != null)
                interactable.enabled = false;
        }
    }

    public void ActivateScripts()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            Outline outline = obj.GetComponent<Outline>();
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

            if (outline != null)
                outline.enabled = true;

            if (interactable != null)
                interactable.enabled = true;
        }
    }
}
