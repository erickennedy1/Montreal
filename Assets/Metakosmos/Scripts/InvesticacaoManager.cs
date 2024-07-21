using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigacaoManager : MonoBehaviour
{
    // Array de sprites que representam diferentes estados do jogo
    public Sprite[] estadoSprites;
    // Referência ao componente Image onde a sprite será alterada
    public Image estadoImage;
    public GameObject Conclusao;

    private int estadoAtual = 0;
    private int totalEstados;

    void Start()
    {
        totalEstados = estadoSprites.Length;
        UpdateEstadoImage();
        Conclusao.SetActive(false);
    }

    public void AvancarEstado()
    {
        if (estadoAtual < totalEstados - 1)
        {
            estadoAtual++;
            UpdateEstadoImage();
        }

        if (estadoAtual >= totalEstados - 1)
        {
            ConclusaoFinal();
        }
    }

    private void UpdateEstadoImage()
    {
        if (estadoAtual >= 0 && estadoAtual < totalEstados)
        {
            estadoImage.sprite = estadoSprites[estadoAtual];
            estadoImage.color = new Color(estadoImage.color.r, estadoImage.color.g, estadoImage.color.b, 1.0f); // Definir opacidade total
        }
    }

    private void ConclusaoFinal()
    {
        Conclusao.SetActive(true);
    }

    public void ResetEstados()
    {
        estadoAtual = 0;
        UpdateEstadoImage();
        Conclusao.SetActive(false);
    }
}
