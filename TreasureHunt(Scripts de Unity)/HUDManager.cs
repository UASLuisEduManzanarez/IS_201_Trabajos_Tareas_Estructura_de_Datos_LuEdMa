using UnityEngine;
using TMPro; 
using System.Collections.Generic;
using System.Linq; 
using System;

public class HUDManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI InventoryText; 
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LevelText;

    [Header("Panel References (Opcional)")]
    public GameObject GameOverPanel; 
    public GameObject WinPanel; 
    

    private const int MAX_INVENTORY_SLOTS = 4; 


    private PlayerController player;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    
    void Start()
    {
        if (player == null)
        {
             player = FindAnyObjectByType<PlayerController>();
        }


        PlayerController.OnLivesChanged += UpdateLivesDisplay;
        PlayerController.OnEnergyChanged += UpdateEnergyDisplay;
        PlayerController.OnScoreChanged += UpdateScoreDisplay;
        

        PlayerController.OnInventoryChanged += UpdateInventoryDisplay; 


        GameManager.OnLevelChanged += UpdateLevelDisplay;
        GameManager.OnGameOver += ShowGameOverPanel;
        GameManager.OnGameWin += ShowWinPanel;

        if (GameOverPanel != null) GameOverPanel.SetActive(false);
        if (WinPanel != null) WinPanel.SetActive(false);
    }
    
    private void OnDestroy()
    {
        PlayerController.OnLivesChanged -= UpdateLivesDisplay;
        PlayerController.OnEnergyChanged -= UpdateEnergyDisplay;
        PlayerController.OnScoreChanged -= UpdateScoreDisplay;
        PlayerController.OnInventoryChanged -= UpdateInventoryDisplay;

        GameManager.OnLevelChanged -= UpdateLevelDisplay;
        GameManager.OnGameOver -= ShowGameOverPanel;
        GameManager.OnGameWin -= ShowWinPanel;
    }
    
    

    void UpdateLivesDisplay(int lives)
    {
        if (LivesText != null)
        {
            LivesText.text = $"Vidas: {lives}";
        }
    }

    void UpdateEnergyDisplay(int currentEnergy)
    {
        if (EnergyText != null)
        {
            int maxEnergy = player != null ? player.maxEnergy : 4; 
            EnergyText.text = $"Energía: {currentEnergy}/{maxEnergy}";
        }
    }
    
    void UpdateInventoryDisplay(int currentCount) 
    {
        if (InventoryText != null)
        {
            InventoryText.text = $"Inventario: {currentCount}/{MAX_INVENTORY_SLOTS}";
        }
    }

    void UpdateScoreDisplay(int score)
    {
        if (ScoreText != null)
        {
            ScoreText.text = $"Puntuación: {score}";
        }
    }
    
    void UpdateLevelDisplay(int level)
    {
        if (LevelText != null)
        {
            LevelText.text = $"Nivel: {level}";
        }
    }
    

    private void ShowGameOverPanel() 
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
    
    private void ShowWinPanel() 
    {
        if (WinPanel != null)
        {
            WinPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}