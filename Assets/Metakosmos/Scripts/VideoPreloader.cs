using UnityEngine;
using UnityEngine.Video;

public class VideoPreloader : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Subscribe to the prepare completed event
        videoPlayer.prepareCompleted += OnPrepareCompleted;

        // Preload the video
        videoPlayer.Prepare();
    }

    void OnPrepareCompleted(VideoPlayer vp)
    {
        Debug.Log("Video is preloaded and ready to play.");

        // Optionally, you can start the video here or whenever needed
        // vp.Play();
    }

    void OnDestroy()
    {
        // Unsubscribe from the prepare completed event
        videoPlayer.prepareCompleted -= OnPrepareCompleted;
    }
}
