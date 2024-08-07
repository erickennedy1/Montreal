using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject repetirButton;
    public GameObject avancarButton;
    public InputActionProperty resetAction;
    public InputActionProperty playAction;

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

        if (repetirButton != null)
        {
            repetirButton.GetComponent<Button>().onClick.AddListener(RepeatVideo);
        }

        if (avancarButton != null)
        {
            avancarButton.GetComponent<Button>().onClick.AddListener(() => GetComponent<FadeManager>().StartFade());
        }
    }

    private void OnEnable()
    {
        resetAction.action.performed += OnResetActionPerformed;
        resetAction.action.Enable();

        playAction.action.performed += OnPlayActionPerformed;
        playAction.action.Enable();
    }

    private void OnDisable()
    {
        resetAction.action.performed -= OnResetActionPerformed;
        resetAction.action.Disable();

        playAction.action.performed -= OnPlayActionPerformed;
        playAction.action.Disable();
    }

    void OnDestroy()
    {
        resetAction.action.performed -= OnResetActionPerformed;
        playAction.action.performed -= OnPlayActionPerformed;
    }

    void OnResetActionPerformed(InputAction.CallbackContext context)
    {
        RepeatVideo();
    }

    void OnPlayActionPerformed(InputAction.CallbackContext context)
    {
        GetComponent<FadeManager>().StartFade();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Lógica adicional ao final do vídeo, se necessário
    }

    public void RepeatVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
    }
}
