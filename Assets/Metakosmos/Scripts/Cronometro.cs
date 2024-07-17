using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public float totalTime = 300f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    public GameObject jogadorPerdeu;
    public GameObject Evidencias;

    void Start()
    {
        ResetTimer();
        jogadorPerdeu.SetActive(false);
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            { 
                currentTime = 0;
                jogadorPerdeu.SetActive(true);
                Evidencias.SetActive(false);
            }
        }

        UpdateTimerDisplay();
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime < 120) 
        {
            timerText.text = $"<size=24>TEMPO RESTANTE</size>\n<size=30><color=red>{timeText}</color></size>";
        }
        else
        {
            timerText.text = $"<size=24>TEMPO RESTANTE</size>\n<size=30><color=green>{timeText}</color></size>";
        }
    }
}
