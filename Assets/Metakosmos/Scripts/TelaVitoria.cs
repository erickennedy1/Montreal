using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TelaVitoria : MonoBehaviour
{
    public InputActionProperty ConcluirAction;
    public GameObject proximaTela; 
    public GameObject telaAtual;

    private void OnEnable()
    {
        ConcluirAction.action.Enable();
        Debug.Log("OnEnable chamado e ação registrada.");
    }

    private void OnDisable()
    {
        ConcluirAction.action.Disable();
        Debug.Log("OnDisable chamado e ação desregistrada.");
    }

    private void Update()
    {
        if (ConcluirAction.action.triggered)
        {
            Debug.Log("Ação Concluir foi disparada no Update.");
            proximaTela.SetActive(true);
            telaAtual.SetActive(false);
        }
    }
}
