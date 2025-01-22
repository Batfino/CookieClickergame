using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenResolutionManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Camera mainCamera;

    // Penge-generering
    public float currentMoney = 0f;
    public float moneyPerSecond = 1f; // Juster dette efter behov

    // Standardopløsning (designopløsning)
    public Vector2 referenceResolution = new Vector2(1080, 1920);

    private static ScreenResolutionManager instance;

    private void Awake()
    {
        // Sørg for, at kun én instans af scriptet eksisterer
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Gør GameObjectet persistent
        }
        else
        {
            Destroy(gameObject); // Slet duplikater
            return;
        }

        // Kør skærmtilpasning ved start
        AdjustResolution();
    }

    private void OnEnable()
    {
        // Kør skærmtilpasning, når scenen ændres
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // Opdater penge-generering hver frame
        currentMoney += moneyPerSecond * Time.deltaTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Opdater referencer til nye Canvas og Camera i den indlæste scene
        mainCanvas = FindObjectOfType<Canvas>();
        mainCamera = Camera.main;

        // Kør skærmtilpasning
        AdjustResolution();
    }

    private void AdjustResolution()
    {
        // Hent skærmens størrelse og aspect ratio
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenAspectRatio = screenWidth / screenHeight;

        // Log skærmoplysninger (til debugging)
        Debug.Log($"Skærmstørrelse: {screenWidth}x{screenHeight}, Aspect Ratio: {screenAspectRatio}");

        // Hvis du bruger UI:
        if (mainCanvas != null)
        {
            CanvasScaler canvasScaler = mainCanvas.GetComponent<CanvasScaler>();
            if (canvasScaler != null)
            {
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = referenceResolution;

                // Automatisk tilpasning baseret på aspect ratio
                if (screenAspectRatio > 1) // Landskab (PC/laptop)
                {
                    canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    canvasScaler.matchWidthOrHeight = 1; // Fokusér på bredden
                }
                else // Portræt (telefon)
                {
                    canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    canvasScaler.matchWidthOrHeight = 0; // Fokusér på højden
                }
            }
        }

        // Hvis du bruger kamera:
        if (mainCamera != null)
        {
            // Tilpas kameraets aspect ratio
            mainCamera.aspect = screenAspectRatio;

            // Juster kameraets størrelse for 2D-spil
            if (mainCamera.orthographic)
            {
                float targetOrthographicSize = 5f; // Standardstørrelse (ændres efter behov)
                if (screenAspectRatio > 1) // Landskab
                {
                    mainCamera.orthographicSize = targetOrthographicSize * (referenceResolution.y / screenHeight);
                }
                else // Portræt
                {
                    mainCamera.orthographicSize = targetOrthographicSize * (referenceResolution.x / screenWidth);
                }
            }
        }
    }
} 