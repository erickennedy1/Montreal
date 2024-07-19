using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public GameObject targetObject;

    private bool isPlayerInside = false;

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned in the Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger");
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            targetObject.SetActive(false);
            Debug.Log("Player entered the trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited the trigger");
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            targetObject.SetActive(true);
            Debug.Log("Player exited the trigger");
        }
    }
}
