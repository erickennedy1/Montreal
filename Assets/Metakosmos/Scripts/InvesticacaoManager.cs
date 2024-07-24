using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigacaoManager : MonoBehaviour
{
    public Sprite[] estadoSprites;
    public Image estadoImage;
    public GameObject Conclusao;
    public GameObject Timer;

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
            estadoImage.color = new Color(estadoImage.color.r, estadoImage.color.g, estadoImage.color.b, 1.0f); 
        }
    }

    private void ConclusaoFinal()
    {
        Conclusao.SetActive(true);
        Timer.SetActive(false);
    }

    public void ResetEstados()
    {
        estadoAtual = 0;
        UpdateEstadoImage();
        Conclusao.SetActive(false);
    }
}
