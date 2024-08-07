using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ComecarExperiencia : MonoBehaviour
{
    public InputActionProperty ButtonAction;
    public CanvasGroup targetCanvasGroup;
    public float fadeDuration = 2.0f;
    public GameObject proxEtapa;
    public AudioSource audioSource; 
    public AudioClip audioClip; 

    public AudioSource secondAudioSource; // Novo AudioSource
    public bool playSecondAudio = false; // Boolean para controlar o segundo AudioSource

    public void Start()
    {
        proxEtapa.SetActive(false);
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
        Debug.Log("Botão pressionado.");
        StartCoroutine(FadeOutCanvasGroup(targetCanvasGroup, fadeDuration));
        
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }

        if (playSecondAudio && secondAudioSource != null)
        {
            secondAudioSource.Play();
        }
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
        }

        proxEtapa.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
