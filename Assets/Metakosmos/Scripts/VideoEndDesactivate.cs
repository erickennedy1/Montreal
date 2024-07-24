using UnityEngine.Video;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class VideoEndDesactivate : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Repetir;
    public GameObject Avancar;

    public InputActionProperty ResetAction;
    public InputActionProperty PlayAction;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    private bool videoEnded = false;

    public bool proximaEtapa = true;
    public GameObject proximoCanvas;

    public AudioSource audioSource;
    public AudioSource secondAudioSource; // Segundo AudioSource
    public Image targetImage;
    public float imageFadeDuration = 1.0f;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer não encontrado!");
        }

        if (Repetir != null)
        {
            Repetir.SetActive(false);
        }

        if (Avancar != null)
        {
            Avancar.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ResetAction.action.performed += OnResetActionPerformed;
        ResetAction.action.Enable();

        PlayAction.action.performed += OnPlayActionPerformed;
        PlayAction.action.Enable();
    }

    private void OnDisable()
    {
        ResetAction.action.performed -= OnResetActionPerformed;
        ResetAction.action.Disable();

        PlayAction.action.performed -= OnPlayActionPerformed;
        PlayAction.action.Disable();
    }

    void OnDestroy()
    {
        ResetAction.action.performed -= OnResetActionPerformed;
        PlayAction.action.performed -= OnPlayActionPerformed;
    }

    void OnResetActionPerformed(InputAction.CallbackContext context)
    {
        if (videoEnded)
        {
            RepeatVideo();
        }
    }

    void OnPlayActionPerformed(InputAction.CallbackContext context)
    {
        if (videoEnded)
        {
            StartCoroutine(FadeCanvasGroup());
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoEnded = true;

        if (Repetir != null)
        {
            Repetir.SetActive(true);
        }

        if (Avancar != null)
        {
            Avancar.SetActive(true);
        }
    }

    public void RepeatVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            videoEnded = false;

            if (Repetir != null)
            {
                Repetir.SetActive(false);
            }

            if (Avancar != null)
            {
                Avancar.SetActive(false);
            }
        }
    }

    private IEnumerator FadeCanvasGroup()
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        if (proximaEtapa)
        {
            proximoCanvas.SetActive(true);
        }

        canvasGroup.alpha = 0;

        if (targetImage != null)
        {
            yield return StartCoroutine(FadeImage());
        }

        PlayAudio();
        this.gameObject.SetActive(false);
    }

    private IEnumerator FadeImage()
    {
        Color startColor = targetImage.color;
        float rate = 1.0f / imageFadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(startColor.a, 0, progress);
            targetImage.color = newColor;
            progress += rate * Time.deltaTime;
            yield return null;
        }

        Color finalColor = targetImage.color;
        finalColor.a = 0;
        targetImage.color = finalColor;
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (secondAudioSource != null)
        {
            secondAudioSource.Play();
            StartCoroutine(StopSecondAudioAfterDelay(0.1f)); 
        }
    }

    private IEnumerator StopSecondAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (secondAudioSource != null)
        {
            secondAudioSource.Stop();
        }
    }
}
