using UnityEngine;

public class LineToggle : MonoBehaviour
{
    public GameObject lineVisualizer;  // Objeto com o script de linha

    private bool isLineVisible;

    void Start()
    {
        // Inicializar o estado da linha
        isLineVisible = lineVisualizer.activeSelf;

        // Debugging inicial
        Debug.Log("Inicialização - Linha ativa: " + lineVisualizer.activeSelf);
    }

    public void ToggleLine()
    {
        // Alterna a visibilidade da linha
        isLineVisible = !isLineVisible;

        // Ativa/desativa a linha
        lineVisualizer.SetActive(isLineVisible);

        // Debugging após alternância
        Debug.Log("Após ToggleLine - Linha ativa: " + lineVisualizer.activeSelf);
    }
}