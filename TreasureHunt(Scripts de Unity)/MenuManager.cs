using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startMenuPanel; 
    
    [Header("Buttons")]
    public GameObject continueButtonGO; 

    void Start()
    {
        Time.timeScale = 0f; 
        
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(true);
        }
        
        CheckContinueButton();
    }

    void CheckContinueButton()
    {
        bool hasSavedGame = PlayerPrefs.HasKey("SavedLevel"); 
        
        if (continueButtonGO != null)
        {
            continueButtonGO.SetActive(hasSavedGame);
        }
    }

    public void StartNewGame()
    {
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(); 
        }
    }
    
    public void ContinueGame()
    {
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
        }
        
        GameDataSaver dataSaver = Object.FindFirstObjectByType<GameDataSaver>();
        if (dataSaver != null)
        {
            dataSaver.LoadGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}