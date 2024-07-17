using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvesticacaoManager : MonoBehaviour
{
    public TextMeshProUGUI evidenciasText; 
    private int evidenciasTotal = 5;
    public GameObject Conclusao;

    private int evidenciasAnalisadas = 0;
    private HashSet<GameObject> objetosAnalisados = new HashSet<GameObject>();

    void Start()
    {
        UpdateEvidenceText();
        Conclusao.SetActive(false);
    }

    public void AnalyzeEvidence(GameObject evidence)
    {
        if (!objetosAnalisados.Contains(evidence))
        {
            objetosAnalisados.Add(evidence);
            evidenciasAnalisadas++;
            UpdateEvidenceText();
        }
        if (evidenciasAnalisadas >= evidenciasTotal)
        {
            ConclusaoFinal();
        }
    }

    private void UpdateEvidenceText()
    {
        evidenciasText.text = $"EVIDÊNCIAS ANALISADAS\n{evidenciasAnalisadas}/{evidenciasTotal}";
    }

    private void ConclusaoFinal()
    {
        Conclusao.SetActive(true);
    }

    public void ResetEvidences()
    {
        evidenciasAnalisadas = 0;
        objetosAnalisados.Clear();
        UpdateEvidenceText();
        Conclusao.SetActive(false);
    }
}
