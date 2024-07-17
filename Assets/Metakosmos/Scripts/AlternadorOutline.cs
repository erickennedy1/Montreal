using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternadorOutline : MonoBehaviour
{
    // Refer�ncia ao objeto que possui o script Outline
    public GameObject targetObject;

    // Refer�ncia ao script Outline
    private Outline outlineScript;

    // Intervalo de tempo para alternar entre os estados
    public float toggleInterval = 1.0f;

    private void Start()
    {
        // Obt�m o componente Outline do objeto alvo
        if (targetObject != null)
        {
            outlineScript = targetObject.GetComponent<Outline>();
            if (outlineScript != null)
            {
                // Inicia a corrotina para alternar o estado do script Outline
                StartCoroutine(ToggleOutlineState());
            }
            else
            {
                Debug.LogError("O componente Outline n�o foi encontrado no objeto alvo.");
            }
        }
        else
        {
            Debug.LogError("O objeto alvo n�o foi definido.");
        }
    }

    private IEnumerator ToggleOutlineState()
    {
        while (true)
        {
            // Alterna a ativa��o do script Outline
            outlineScript.enabled = !outlineScript.enabled;

            // Aguarda o intervalo especificado antes de alternar novamente
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
