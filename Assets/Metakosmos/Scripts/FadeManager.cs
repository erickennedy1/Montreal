using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public GameObject proximoCanvas;
    public AudioSource audioSource;
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public Image targetImage;
    public float imageFadeDuration = 1.0f;
    public AudioSource newAudioSource; 
    public AudioClip newAudioClip;

    public void StartFade()
    {
        StartCoroutine(FadeCanvasGroup());
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

        if (proximoCanvas != null)
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
        PlayNewAudio(); // Iniciar o novo áudio
        gameObject.SetActive(false);
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

    private void PlayNewAudio()
    {
        if (newAudioSource != null && newAudioClip != null)
        {
            newAudioSource.clip = newAudioClip;
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
