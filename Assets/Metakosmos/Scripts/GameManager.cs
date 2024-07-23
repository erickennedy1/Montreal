using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public InvestigacaoManager investigationManager;

    public void RestartGame()
    {
        investigationManager.ResetEstados();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
