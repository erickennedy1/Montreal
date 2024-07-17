using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class VerificaEntradaXR : MonoBehaviour
{
    public InputActionProperty ButtonAction;
    public CanvasGroup targetCanvasGroup;
    public float fadeDuration = 1.0f;

    public GameObject timerEvidencias;

    public void Start()
    {
        timerEvidencias.SetActive(false);
    }

    private void OnEnable()
    {
        ButtonAction.action.performed += OnButtonPressed;
        ButtonAction.action.Enable();
    }

    private void OnDisable()
    {
        ButtonAction.action.performed -= OnButtonPressed;
        ButtonAction.action.Disable();
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        StartCoroutine(FadeOutCanvasGroup(targetCanvasGroup, fadeDuration));
    }


    private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, float duration)
    {
        if (canvasGroup != null)
        {
            float startAlpha = canvasGroup.alpha;
            float endAlpha = 0f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                yield return null;
            }

            canvasGroup.alpha = endAlpha;

            timerEvidencias.SetActive(true);
        }
    }
}
