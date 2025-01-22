using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    
    [SerializeField] public int maxUnits = 10;
    private int boughtUnits = 0;
    private int activeUnits = 0;
    
    public UnitSpawner unitSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BuyUnit()
    {
        if (boughtUnits < maxUnits)
        {
            boughtUnits++;
            SaveGameData();
            TrySpawnUnit();
        }
        else
        {
            Debug.Log("Max units reached! Cannot buy more units.");
        }
    }

    public void UnitDied()
    {
        if (activeUnits > 0)
        {
            activeUnits--;
            SaveGameData();
            TrySpawnUnit();
        }
    }

    private void TrySpawnUnit()
    {
        if (activeUnits < maxUnits && boughtUnits > activeUnits)
        {
            if (unitSpawner != null)
            {
                unitSpawner.SpawnUnit();
                activeUnits++;
                SaveGameData();
            }
        }
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("BoughtUnits", boughtUnits);
        PlayerPrefs.SetInt("ActiveUnits", activeUnits);
        PlayerPrefs.Save();
    }

    private void LoadGameData()
    {
        boughtUnits = PlayerPrefs.GetInt("BoughtUnits", 0);
        activeUnits = PlayerPrefs.GetInt("ActiveUnits", 0);
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteKey("BoughtUnits");
        PlayerPrefs.DeleteKey("ActiveUnits");
        boughtUnits = 0;
        activeUnits = 0;
        
        // Slet alle eksisterende enheder på tværs af alle scener
        var units = FindObjectsOfType<Unit>(true);
        foreach (var unit in units)
        {
            if (unit != null && unit.gameObject != null)
            {
                Destroy(unit.gameObject);
            }
        }
        
        // Tjek om UnitSpawner eksisterer og nulstil den
        if (unitSpawner != null)
        {
            unitSpawner.ResetSpawner();
        }
        
        // Gem de nulstillede værdier
        SaveGameData();
        
        Debug.Log("Manager data reset complete!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Tilføj en metode til at sætte UnitSpawner
    public void SetUnitSpawner(UnitSpawner spawner)
    {
        unitSpawner = spawner;
    }

    public int GetUnitsToSpawn()
    {
        return boughtUnits - activeUnits;
    }
}
