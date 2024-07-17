using UnityEngine;

public class InstrucoesControl : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject botao1; 
    public GameObject botao2; 

    private bool audioWasPlaying;

    void Start()
    {
        botao1.SetActive(false);
        botao2.SetActive(false);

        audioWasPlaying = audioSource.isPlaying;
    }

    void Update()
    {
        if (audioWasPlaying && !audioSource.isPlaying)
        {
            EnableObjects();
            audioWasPlaying = false; 
        }

        if (!audioWasPlaying && audioSource.isPlaying)
        {
            audioWasPlaying = true;
        }
    }

    private void EnableObjects()
    {
        botao1.SetActive(true);
        botao2.SetActive(true);
    }

    public void RepeatAudio()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
