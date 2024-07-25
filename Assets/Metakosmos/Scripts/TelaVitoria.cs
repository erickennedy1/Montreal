using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TelaVitoria : MonoBehaviour
{
    public InputActionProperty ConcluirAction;
    public GameObject proximaTela;

    private bool isReady = false;

    private void OnEnable()
    {
        ConcluirAction.action.Enable();
        Debug.Log("OnEnable chamado e ação registrada.");
        StartCoroutine(WaitAndEnable());
    }

    private void OnDisable()
    {
        ConcluirAction.action.Disable();
        Debug.Log("OnDisable chamado e ação desregistrada.");
    }

    private void Update()
    {
        if (isReady && ConcluirAction.action.triggered)
        {
            Debug.Log("Ação Concluir foi disparada no Update.");
            proximaTela.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitAndEnable()
    {
        isReady = false;
        yield return new WaitForSeconds(0.1f); 
        isReady = true;
    }
}
