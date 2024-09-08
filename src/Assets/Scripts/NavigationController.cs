using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour
{
    public Vector3 TargetPosition { get; set; } = Vector3.zero;
    public NavMeshPath CalculatedPath { get; private set; }

    void Start()
    {
        CalculatedPath = new NavMeshPath();
    }

    public void CalculatePath()
    {
        if (TargetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, TargetPosition, NavMesh.AllAreas, CalculatedPath);
        }
    }
}