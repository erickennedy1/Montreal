using System.Collections;
using UnityEngine;

public class ActivateObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToActivate;

    void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); 
            StartCoroutine(ActivateAfterDelay(1.0f)); 
        }
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); 
        }
    }
}
