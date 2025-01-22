using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLoader : MonoBehaviour
{
    public GameObject managerPrefab;

    void Awake()
    {
        // Tjek om Manager allerede eksisterer
        if (Manager.Instance == null)
        {
            // Instantier Manager fra prefab
            Instantiate(managerPrefab);
        }
    }
} 