using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConcluirExperiencia : MonoBehaviour
{
    public InputActionProperty botaoFinalizacao;
    public InputActionProperty botaoFecharJogo;
    public GameManager gameManager;

    public AudioSource audioSource; 
    public AudioClip novoAudioClip;

    private bool podeConcluir = true; 
    private bool iniciou = false;
    private bool isReady = false;

    private void OnEnable()
    {
        botaoFinalizacao.action.performed += OnConcluirPerformed;
        botaoFinalizacao.action.Enable();

        botaoFecharJogo.action.performed += OnFecharJogoPerformed;
        botaoFecharJogo.action.Enable();

        StartCoroutine(WaitAndEnable());

        TrocarEAjustarAudio();
    }

    private void OnDisable()
    {
        botaoFinalizacao.action.performed -= OnConcluirPerformed;
        botaoFinalizacao.action.Disable();

        botaoFecharJogo.action.performed -= OnFecharJogoPerformed;
        botaoFecharJogo.action.Disable();
    }

    private void OnDestroy()
    {
        botaoFinalizacao.action.performed -= OnConcluirPerformed;
        botaoFecharJogo.action.performed -= OnFecharJogoPerformed;
    }

    private void Update()
    {
        if (!iniciou)
        {
            iniciou = true;
        }
    }

    private void OnConcluirPerformed(InputAction.CallbackContext context)
    {
        if (isReady && podeConcluir && iniciou)
        {
            StartCoroutine(ConcluirComAtraso());
        }
    }

    private IEnumerator ConcluirComAtraso()
    {
        podeConcluir = false;

        yield return new WaitForSeconds(0.5f); 

        gameManager.RestartGame();
        gameObject.SetActive(false);

        podeConcluir = true;
    }

    private void OnFecharJogoPerformed(InputAction.CallbackContext context)
    {
        if (isReady)
        {
            FecharJogo();
        }
    }

    private void FecharJogo()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator WaitAndEnable()
    {
        isReady = false;
        yield return new WaitForSeconds(0.1f); 
        isReady = true;
    }

    private void TrocarEAjustarAudio()
    {
        if (audioSource != null && novoAudioClip != null)
        {
            audioSource.clip = novoAudioClip;
            audioSource.Play();
        }
    }
}
