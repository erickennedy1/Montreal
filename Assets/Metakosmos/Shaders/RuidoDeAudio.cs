using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RuidoDeAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public Image barPrefab; // Referência à barra original
    public Transform barsParent; // Objeto pai contendo todas as imagens
    public int numberOfBars = 64; // Número de barras a serem criadas
    public float maxScale = 200f;
    public float barSpacing = 30f; // Espaçamento entre as barras
    private Image[] bars;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bars = new Image[numberOfBars];

        // Posição inicial da barra original
        Vector3 startPosition = barPrefab.rectTransform.anchoredPosition;

        // Criar e posicionar as novas barras
        for (int i = 0; i < numberOfBars; i++)
        {
            Image newBar = Instantiate(barPrefab, barsParent);
            newBar.rectTransform.anchoredPosition = startPosition + new Vector3(i * barSpacing, 0, 0);
            bars[i] = newBar;
        }

        // Desativar a barra original
        barPrefab.gameObject.SetActive(false);
    }

    void Update()
    {
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // Para depuração, imprime alguns valores do espectro no console
        Debug.Log("Spectrum values: " + string.Join(", ", spectrum.Take(numberOfBars)));

        for (int i = 0; i < numberOfBars; i++)
        {
            if (bars[i] != null)
            {
                // Para depuração, imprime o valor do espectro e a escala calculada
                Debug.Log($"Bar {i}, Spectrum: {spectrum[i]}, Scale: {Mathf.Clamp(spectrum[i] * maxScale, 0.1f, maxScale)}");

                float scale = Mathf.Clamp(spectrum[i] * maxScale, 0.1f, maxScale);
                bars[i].rectTransform.sizeDelta = new Vector2(bars[i].rectTransform.sizeDelta.x, scale);
            }
        }
    }
}
