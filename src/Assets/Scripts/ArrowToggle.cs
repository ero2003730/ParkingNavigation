using UnityEngine;

public class ArrowToggle : MonoBehaviour
{
    public GameObject arrowVisualizer;  // Objeto com o script de setas

    private bool isArrowVisible;

    void Start()
    {
        // Inicializar o estado da seta
        isArrowVisible = arrowVisualizer.activeSelf;

        // Debugging inicial
        Debug.Log("Inicialização - Seta ativa: " + arrowVisualizer.activeSelf);
    }

    public void ToggleArrow()
    {
        // Alterna a visibilidade da seta
        isArrowVisible = !isArrowVisible;

        // Ativa/desativa a seta
        arrowVisualizer.SetActive(isArrowVisible);

        // Debugging após alternância
        Debug.Log("Após ToggleArrow - Seta ativa: " + arrowVisualizer.activeSelf);
    }
}