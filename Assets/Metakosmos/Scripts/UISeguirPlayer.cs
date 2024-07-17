using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISeguirPlayer : MonoBehaviour
{
    private Transform player;
    public bool mudarPosicao = false; 
    private Vector3 distanciaInicial;
    private float Suavizacao = 0.3f;
    private Vector3 velocity = Vector3.zero; 

    void Start()
    {
        player = Camera.main.transform;
        distanciaInicial = transform.position - player.position; 
    }

    void Update()
    {
        transform.LookAt(player);

        if (mudarPosicao)
        {
            Vector3 targetPosition = player.position + player.forward * distanciaInicial.magnitude;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Suavizacao);
        }
    }
}
