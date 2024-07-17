using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject proximaEtapa;
    public GameObject painelAtual;

    void Start()
    {
        proximaEtapa.SetActive(false);
        painelAtual.SetActive(true);

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        proximaEtapa.SetActive(true);
        painelAtual.SetActive(false);
    }

    void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
