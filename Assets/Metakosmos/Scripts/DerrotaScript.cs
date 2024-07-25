using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DerrotaScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<GameObject> objetosParaDesativar;
    public GameObject objetoParaAtivar;
    public AudioSource audioSource; 
    public AudioClip novoAudioClip;

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer não atribuído!");
            return;
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Vídeo terminou, alterando estado dos objetos.");

        if (audioSource != null && novoAudioClip != null)
        {
            audioSource.clip = novoAudioClip;
            audioSource.Play();
        }

        foreach (GameObject obj in objetosParaDesativar)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        if (objetoParaAtivar != null)
        {
            objetoParaAtivar.SetActive(true);
        }

    }
}
