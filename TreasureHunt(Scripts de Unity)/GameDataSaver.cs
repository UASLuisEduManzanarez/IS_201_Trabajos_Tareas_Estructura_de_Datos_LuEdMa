
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class GameDataSaver : MonoBehaviour
{
    private string saveFilePath;
    
    void Awake()
    {
        
        saveFilePath = Path.Combine(Application.persistentDataPath, "gamedata.txt");
    }

    public void SaveGame()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        GameManager gm = GameManager.Instance;
        
        if (player == null || gm == null) return;

        
        List<string> inventoryKeys = new List<string>();
        if (player.inventoryItems != null)
        {
            foreach (WorldItem item in player.inventoryItems)
            {
                if (item != null && item.gameObject != null)
                {
                    inventoryKeys.Add(item.gameObject.name); 
                }
            }
        }
      
        string inventoryString = string.Join(",", inventoryKeys);

     
        List<string> dataToSave = new List<string>
        {
            // Índice 0: Nivel actual
            gm.currentLevel.ToString(),
            // Índice 1: Vidas
            player.lives.ToString(),
            // Índice 2: Energía
            player.energy.ToString(),
            // Índice 3: Puntuación
            player.Score.ToString(),
            // Índice 4: Inventario serializado (strings)
            inventoryString, 
        };

        try
        {
            File.WriteAllLines(saveFilePath, dataToSave);
            gm.ShowNotification("Juego guardado con éxito.", 1.5f);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al guardar el juego: {e.Message}");
            gm.ShowNotification("Error al guardar.", 1.5f);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            GameManager.Instance.ShowNotification("No hay partida guardada.", 1.5f);
            return;
        }

        try
        {
            string[] loadedData = File.ReadAllLines(saveFilePath);
            if (loadedData.Length < 5) 
            {
                GameManager.Instance.ShowNotification("Archivo de guardado corrupto.", 1.5f);
                return;
            }

            int loadedLevel = int.Parse(loadedData[0]);
            int loadedLives = int.Parse(loadedData[1]);
            int loadedEnergy = int.Parse(loadedData[2]);
            int loadedScore = int.Parse(loadedData[3]);
            
            string inventoryString = loadedData[4]; 
            
            List<String> savedInventoryKeys = inventoryString
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            GameManager gm = GameManager.Instance;
            if (gm != null)
           if (gm != null)
{
    gm.currentLevel = loadedLevel; 
    
    gm.ClearBoard();
    gm.GenerateBoard(); 


    gm.SpawnPlayerForNewLevel(loadedLives, loadedEnergy, loadedScore, savedInventoryKeys);
    
    GameManager.OnLevelChanged?.Invoke(gm.currentLevel); 

    gm.ShowNotification("Partida cargada.", 1.5f);
    
    gm.gameTimer?.StartTimer(gm.currentLevel);
}
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al cargar el juego: {e.Message}");
            GameManager.Instance.ShowNotification("Error al cargar.", 1.5f);
        }
    }
}