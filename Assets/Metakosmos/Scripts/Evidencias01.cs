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
        Concluir();
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
}
