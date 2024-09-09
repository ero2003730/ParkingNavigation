using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;   // Campo de texto para entrada
    [SerializeField]
    private GameObject openInputButton;  // Botão que abre o InputField

    [Header("Stored Input Value")]
    private string inputText;  // Texto armazenado

    private void Start()
    {
        // Inicialmente, esconda o campo de texto
        inputField.gameObject.SetActive(false);

        // Adicionar listener ao botão para abrir o campo de texto
        openInputButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OpenInputField);

        // Adicionar listener ao InputField para fechar o campo de texto e salvar o texto quando o usuário apertar Enter ou clicar em OK
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

   private void OpenInputField()
{
    openInputButton.SetActive(false); // Esconder o botão principal
    inputField.gameObject.SetActive(true); // Mostrar o campo de texto

    // Redefinir o texto do InputField para o valor padrão
    inputField.text = "";  // Limpar o texto anterior
    inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Digite a Vaga Desejada";  // Definir o texto do placeholder

    // O foco no InputField será dado apenas quando o usuário clicar nele, então removemos o ActivateInputField.
    Debug.Log("InputField deve estar visível agora.");
}

    private void OnEndEdit(string input)
    {
        // Salva o texto inserido
        inputText = input;
        Debug.Log("Edição finalizada com: " + inputText);

        // Fecha o InputField
        CloseInputField();
    }

    private void CloseInputField()
    {
        inputField.gameObject.SetActive(false);  // Esconder o campo de texto
        openInputButton.SetActive(true); // Mostrar o botão principal novamente
    }

    // Função pública para obter o valor do input
    public string GetInputValue()
    {
        return inputText;
    }
}