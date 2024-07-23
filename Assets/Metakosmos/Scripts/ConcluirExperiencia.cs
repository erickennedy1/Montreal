using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConcluirExperiencia : MonoBehaviour
{
    public InputActionProperty botaoFinalizacao;
    public GameManager gameManager;

    private bool podeConcluir = true; 
    private bool iniciou = false;

    private void OnEnable()
    {
        botaoFinalizacao.action.performed += OnConcluirPerformed;
        botaoFinalizacao.action.Enable();
    }

    private void OnDisable()
    {
        botaoFinalizacao.action.performed -= OnConcluirPerformed;
        botaoFinalizacao.action.Disable();
    }

    private void OnDestroy()
    {
        botaoFinalizacao.action.performed -= OnConcluirPerformed;
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
        if (podeConcluir && iniciou)
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
}
