using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    // Metode til at gå til Scene 0 (Butik)
    public void LoadStoreScene()
    {
        // Tjek at vi ikke allerede er i Scene 0
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Metode til at gå til Scene 1 (Spil)
    public void LoadGameScene()
    {
        // Tjek at vi ikke allerede er i Scene 1
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(1);
        }
    }
} 