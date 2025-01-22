using UnityEngine;

public class Unit : MonoBehaviour
{
    private Manager manager;

    void Start()
    {
        manager = Manager.Instance;
    }

    public void Die()
    {
        if (manager != null)
        {
            manager.UnitDied();
        }
        Destroy(gameObject);
    }
} 