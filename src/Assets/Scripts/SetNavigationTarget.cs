using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    [SerializeField]
    private Camera topDownCamera;

    [SerializeField]
    private GameObject pointers;  // Referência ao objeto pai que contém todos os pointers visuais (TargetCubes)
    [SerializeField]
    private GameObject parents;  // Referência ao objeto pai que contém todos os Nav Target Parents

    private List<GameObject> navTargetParents = new List<GameObject>();  // Lista de alvos de navegação (Nav Target Parents)
    private List<GameObject> targetCubes = new List<GameObject>();  // Lista de `TargetCubes` visuais

    [SerializeField]
    private InputManager inputManager;  // Referência ao script InputManager

    [SerializeField]
    private TextMeshProUGUI distanceText;  // Referência ao texto na UI que mostrará a distância

    [SerializeField]
    private NavigationController navigationController; // Referência ao script NavigationController
    private NavMeshPath path; // Caminho atual calculado
    private LineRenderer line; // LineRenderer para exibir o caminho

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = false;

        // Popula as listas de alvos e pointers automaticamente
        PopulateTargetLists();

        // Inicializa todos os pointers (targetCubes) como invisíveis
        SetArrowVisibility(-1);
    }

    private void Update()
    {
        // Acessar o valor do input através do InputManager
        string inputValue = inputManager.GetInputValue();

        int targetSquare;
        if (int.TryParse(inputValue, out targetSquare))
        {
            if (targetSquare > 0 && targetSquare <= navTargetParents.Count)
            {
                int index = targetSquare - 1;

                // Obter o Parent correspondente
                GameObject targetParent = navTargetParents[index];

                // Atualizar o destino no NavigationController
                navigationController.TargetPosition = targetParent.transform.position;
                navigationController.CalculatePath();  // Recalcula o caminho

                // Desenha o caminho no LineRenderer
                path = navigationController.CalculatedPath;
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);
                line.enabled = true;

                // Calcular a distância e exibir na tela
                float distance = CalculateDistanceToTarget(targetParent.transform.position);
                distanceText.text = "Distância até o alvo: " + distance.ToString("F2") + "m";

                // Atualizar a visibilidade dos pointers
                SetArrowVisibility(index);
            }
            else
            {
                Debug.LogError("Alvo não encontrado para o valor: " + targetSquare);
                line.enabled = false;  // Desativa a linha se o valor não for válido
                distanceText.text = "";  // Limpa o texto de distância

                // Ocultar todos os pointers
                SetArrowVisibility(-1);
            }
        }
        else
        {
            line.enabled = false;  // Desativa a linha se o valor não for válido
            distanceText.text = "";  // Limpa o texto de distância

            // Ocultar todos os pointers
            SetArrowVisibility(-1);
        }
    }

    private void PopulateTargetLists()
    {
        // Popula a lista de navTargetParents com os filhos de Parents
        if (parents != null)
        {
            foreach (Transform child in parents.transform)
            {
                navTargetParents.Add(child.gameObject);
            }
            Debug.Log("Nav Target Parents adicionados automaticamente à lista.");
        }
        else
        {
            Debug.LogError("Objeto 'Parents' não foi atribuído no Inspector.");
        }

        // Popula a lista de targetCubes com os filhos de Pointers
        if (pointers != null)
        {
            foreach (Transform child in pointers.transform)
            {
                targetCubes.Add(child.gameObject);
            }
            Debug.Log("Target Cubes adicionados automaticamente à lista.");
        }
        else
        {
            Debug.LogError("Objeto 'Pointers' não foi atribuído no Inspector.");
        }
    }

    private void SetArrowVisibility(int activeArrowIndex)
    {
        // Tornar todas as setas invisíveis, exceto aquela indicada pelo índice
        for (int i = 0; i < targetCubes.Count; i++)
        {
            targetCubes[i].SetActive(i == activeArrowIndex);
        }
    }

    private float CalculateDistanceToTarget(Vector3 targetPosition)
    {
        // Calcula a distância em metros do objeto até o alvo
        return Vector3.Distance(transform.position, targetPosition);
    }

    public void ToggleVisibility()
    {
        line.enabled = !line.enabled;
    }
}