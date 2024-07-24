using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Evidencias : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject repetirButton;
    public GameObject avancarButton;

    public InputActionProperty ResetVideoAction;
    public InputActionProperty AvancarAction;

    private bool videoEnded = false;
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
            Debug.LogError("VideoPlayer n�o encontrado!");
        }

        if (repetirButton != null)
        {
            repetirButton.SetActive(false);
        }

        if (avancarButton != null)
        {
            avancarButton.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ResetVideoAction.action.performed += OnResetVideoPerformed;
        ResetVideoAction.action.Enable();

        AvancarAction.action.performed += OnAvancarPerformed;
        AvancarAction.action.Enable();
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
        if (videoEnded)
        {
            ResetVideo();
        }
    }

    private void OnAvancarPerformed(InputAction.CallbackContext context)
    {
        if (videoEnded)
        {
            Avancar();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoEnded = true;

        if (repetirButton != null)
        {
            repetirButton.SetActive(true);
        }

        if (avancarButton != null)
        {
            avancarButton.SetActive(true);
        }
    }

    private void ResetVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            videoEnded = false;

            if (repetirButton != null)
            {
                repetirButton.SetActive(false);
            }

            if (avancarButton != null)
            {
                avancarButton.SetActive(false);
            }
        }
    }

    private void Avancar()
    {
        if (videoEnded)
        {
            if (!objetosAnalisados.Contains(videoPlayer.gameObject))
            {
                objetosAnalisados.Add(videoPlayer.gameObject);
                investigacaoManager.AvancarEstado();
            }

            videoEnded = false;

            if (repetirButton != null)
            {
                repetirButton.SetActive(false);
            }

            if (avancarButton != null)
            {
                avancarButton.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}
