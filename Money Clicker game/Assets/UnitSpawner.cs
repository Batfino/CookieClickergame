using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform spawnPoint;

    void Start()
    {
        // Kun spawn hvis vi er i Scene 1
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SpawnInitialUnits();
        }
    }

    private void SpawnInitialUnits()
    {
        if (Manager.Instance != null)
        {
            int unitsToSpawn = Manager.Instance.GetUnitsToSpawn();
            for (int i = 0; i < unitsToSpawn; i++)
            {
                SpawnUnit();
            }
        }
    }

    public void SpawnUnit()
    {
        if (spawnPoint != null && unitPrefab != null)
        {
            Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void ResetSpawner()
    {
        // Slet alle spawned units
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        Debug.Log("UnitSpawner reset complete");
    }
} 