using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Initializer : MonoBehaviour
{
    public UnitSpawner unitSpawner;

    void Start()
    {
        // Sæt UnitSpawner reference i Manager
        if (Manager.Instance != null)
        {
            Manager.Instance.SetUnitSpawner(unitSpawner);
        }
    }
} 