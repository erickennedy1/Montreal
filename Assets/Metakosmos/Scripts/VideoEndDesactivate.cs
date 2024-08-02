using UnityEngine.Video;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class VideoInicialControler : MonoBehaviour
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
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public Image targetImage;
    public float imageFadeDuration = 1.0f;

    public AudioSource newAudioSource; // Novo AudioSource para o novo áudio

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
            Repetir.GetComponent<Button>().onClick.AddListener(RepeatVideo);
        }

        if (Avancar != null)
        {
            Avancar.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(FadeCanvasGroup()));
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
        RepeatVideo();
    }

    void OnPlayActionPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(FadeCanvasGroup());
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoEnded = true;
    }

    public void RepeatVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            videoEnded = false;
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
            PlayAudio(firstAudioClip);
            yield return StartCoroutine(FadeImage());
        }

        PlayAudio(secondAudioClip);
        StartNewAudio(); // Iniciar o novo áudio
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

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(StopAudioAfterDelay(audioSource.clip.length));
        }
    }

    private void StartNewAudio()
    {
        if (newAudioSource != null)
        {
            newAudioSource.Play();
        }
    }

    private IEnumerator StopAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
