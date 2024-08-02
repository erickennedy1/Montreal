using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class TelaVitoria : MonoBehaviour
{
    public InputActionProperty ConcluirAction;
    public InputActionProperty RepetirAction; 
    public GameObject proximaTela;
    public VideoPlayer videoPlayer; 

    private bool isReady = false;

    private void OnEnable()
    {
        ConcluirAction.action.Enable();
        RepetirAction.action.Enable(); 
        Debug.Log("OnEnable chamado e ações registradas.");
        StartCoroutine(WaitAndEnable());
    }

    private void OnDisable()
    {
        ConcluirAction.action.Disable();
        RepetirAction.action.Disable(); 
        Debug.Log("OnDisable chamado e ações desregistradas.");
    }

    private void Update()
    {
        if (isReady)
        {
            if (ConcluirAction.action.triggered)
            {
                Debug.Log("Ação Concluir foi disparada no Update.");
                proximaTela.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else if (RepetirAction.action.triggered) 
            {
                Debug.Log("Ação Repetir foi disparada no Update.");
                RepeatVideo();
            }
        }
    }

    private IEnumerator WaitAndEnable()
    {
        isReady = false;
        yield return new WaitForSeconds(0.1f);
        isReady = true;
    }

    private void RepeatVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            Debug.Log("Vídeo repetido.");
        }
        else
        {
            Debug.LogWarning("VideoPlayer não está atribuído.");
        }
    }
}
