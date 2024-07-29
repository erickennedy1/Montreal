using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.UI;

public class Evidencias : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject repetirButton;
    public GameObject avancarButton;

    public InputActionProperty ResetVideoAction;
    public InputActionProperty AvancarAction;

    private bool videoEnded = false;
    private bool isReady = false;
    private HashSet<GameObject> objetosAnalisados = new HashSet<GameObject>();

    public InvestigacaoManager investigacaoManager;

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
            repetirButton.SetActive(true);
            repetirButton.GetComponent<Button>().onClick.AddListener(ResetVideo);
        }

        if (avancarButton != null)
        {
            avancarButton.SetActive(true);
            avancarButton.GetComponent<Button>().onClick.AddListener(Avancar);
        }
    }

    private void OnEnable()
    {
        ResetVideoAction.action.performed += OnResetVideoPerformed;
        ResetVideoAction.action.Enable();

        AvancarAction.action.performed += OnAvancarPerformed;
        AvancarAction.action.Enable();

        StartCoroutine(WaitAndEnable());
    }

    private void OnDisable()
    {
        ResetVideoAction.action.performed -= OnResetVideoPerformed;
        ResetVideoAction.action.Disable();

        AvancarAction.action.performed -= OnAvancarPerformed;
        AvancarAction.action.Disable();
    }

    void OnDestroy()
    {
        ResetVideoAction.action.performed -= OnResetVideoPerformed;
        AvancarAction.action.performed -= OnAvancarPerformed;
    }

    private void OnResetVideoPerformed(InputAction.CallbackContext context)
    {
        if (isReady)
        {
            ResetVideo();
        }
    }

    private void OnAvancarPerformed(InputAction.CallbackContext context)
    {
        if (isReady)
        {
            Avancar();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoEnded = true;
    }

    private void ResetVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            videoEnded = false;
        }
    }

    private void Avancar()
    {
        if (!objetosAnalisados.Contains(videoPlayer.gameObject))
        {
            objetosAnalisados.Add(videoPlayer.gameObject);
            investigacaoManager.AvancarEstado();
        }

        videoEnded = false;
        gameObject.SetActive(false);
    }

    private IEnumerator WaitAndEnable()
    {
        isReady = false;
        yield return new WaitForSeconds(0.1f); 
        isReady = true;
    }
}
