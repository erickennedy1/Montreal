using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public Cronometro timerScript;
    public InvesticacaoManager investigationManager;
    public Transform vrRig; 
    public Transform resetPosition; 

    public void RestartGame()
    {
        timerScript.ResetTimer();

        investigationManager.ResetEvidences();

        ResetVRRigPosition();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetVRRigPosition()
    {
        if (resetPosition != null)
        {
            vrRig.position = resetPosition.position;
            vrRig.rotation = resetPosition.rotation;
        }
    }
}
