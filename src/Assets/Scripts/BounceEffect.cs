using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float amplitude = 0.4f; // A altura máxima do movimento de pingo
    public float frequency = 1f; // A velocidade do movimento de pingo

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition; // Salva a posição inicial do objeto filho
    }

    void Update()
    {
        // Calcula a nova posição Y usando uma função seno para criar o efeito de pingo
        float newY = startPosition.y + Mathf.Abs(Mathf.Sin(Time.time * frequency) * amplitude);
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}