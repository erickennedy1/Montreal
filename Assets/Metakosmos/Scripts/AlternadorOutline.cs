using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternadorOutline : MonoBehaviour
{
    // Referência ao objeto que possui o script Outline
    public GameObject targetObject;

    // Referência ao script Outline
    private Outline outlineScript;

    // Intervalo de tempo para alternar entre os estados
    public float toggleInterval = 1.0f;

    private void Start()
    {
        // Obtém o componente Outline do objeto alvo
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
                Debug.LogError("O componente Outline não foi encontrado no objeto alvo.");
            }
        }
        else
        {
            Debug.LogError("O objeto alvo não foi definido.");
        }
    }

    private IEnumerator ToggleOutlineState()
    {
        while (true)
        {
            // Alterna a ativação do script Outline
            outlineScript.enabled = !outlineScript.enabled;

            // Aguarda o intervalo especificado antes de alternar novamente
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
