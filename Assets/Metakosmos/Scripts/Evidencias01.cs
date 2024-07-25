using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Evidencias01 : MonoBehaviour
{
    public GameObject concluirButton;
    public InputActionProperty ConcluirAction;

    private HashSet<GameObject> objetosAnalisados = new HashSet<GameObject>();
    public InvestigacaoManager investigacaoManager;

    private bool isReady = false;

    void Start()
    {
        if (concluirButton != null)
        {
            concluirButton.SetActive(true);
        }
    }

    private void OnEnable()
    {
        ConcluirAction.action.performed += OnConcluirPerformed;
        ConcluirAction.action.Enable();
        StartCoroutine(WaitAndEnable());
    }

    private void OnDisable()
    {
        ConcluirAction.action.performed -= OnConcluirPerformed;
        ConcluirAction.action.Disable();
    }

    void OnDestroy()
    {
        ConcluirAction.action.performed -= OnConcluirPerformed;
    }

    private void OnConcluirPerformed(InputAction.CallbackContext context)
    {
        if (isReady)
        {
            Concluir();
        }
    }

    private void Concluir()
    {
        if (!objetosAnalisados.Contains(gameObject))
        {
            objetosAnalisados.Add(gameObject);
            investigacaoManager.AvancarEstado();
        }

        concluirButton.SetActive(false);
    }

    private IEnumerator WaitAndEnable()
    {
        isReady = false;
        yield return new WaitForSeconds(0.1f); 
        isReady = true;
    }
}
