using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // --- ESTADÍSTICAS DEL JUGADOR ---
    public int lives = 3;
    public int maxLives = 3; 
    public int energy = 4; 
    public int maxEnergy = 4; 
    public int Score = 0;
    
    // --- ESTADO DEL JUEGO (NUEVO INVENTARIO) ---
    [Header("Inventory")]
    public List<WorldItem> inventoryItems = new List<WorldItem>(); 
    public int maxInventory = 5; 
    
    public int gridX;
    public int gridY;
    
    // --- EVENTOS ---
    public static Action<int> OnLivesChanged; 
    public static Action<int> OnEnergyChanged; 
    public static Action<int> OnScoreChanged; 
    public static Action<int> OnInventoryChanged; 
    public static Action<bool> OnToggleInventory;

    // --- REFERENCIAS ---
    private GameManager gm;
    
    
    void Awake()
    {
        gm = GameManager.Instance;
    }

    void Start()
    {
        if (gm == null)
        {
            gm = GameManager.Instance;
        }
        
        if (gm != null)
        {
            gm.RevealArea(gridX, gridY, 1); 
        }
    }
    
    void Update()
    {
        if (Time.timeScale == 0f) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleInventory();
            }
            return;
        }

        int dx = 0;
        int dy = 0;
        
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            dy = -1; // Arriba
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        {
            dy = 1; // Abajo
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            dx = 1; // Derecha
        }
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            dx = -1; // Izquierda
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
            return; 
        }

        if (dx != 0 || dy != 0)
        {
            Move(dx, dy);
        }

        
    }

    public void InitializeStats(int startLives, int startEnergy, int startScore, List<String> startInventoryKeys)
    {
        this.lives = startLives;
        this.maxLives = 3; 
        this.maxEnergy = 4; 
        this.energy = Mathf.Min(startEnergy, maxEnergy); 
        this.Score = startScore;
        
       if (this.inventoryItems != null)
    {
        foreach (var item in this.inventoryItems)
        {
            if (item != null && item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
    }


    this.inventoryItems = new List<WorldItem>();
    
  
    GameManager gm = GameManager.Instance;
    
    foreach (string key in startInventoryKeys)
    {
        GameObject itemPrefab = gm.GetPrefabByName(key); 
        
        if (itemPrefab != null)
        {
            GameObject newItemGO = Instantiate(itemPrefab, this.transform); 
            WorldItem newItem = newItemGO.GetComponent<WorldItem>();
            
            if (newItem != null)
            {
                this.inventoryItems.Add(newItem);
                newItemGO.SetActive(false); 
            }
            else
            {
                 Destroy(newItemGO);
            }
        }
    }
        
    
        OnLivesChanged?.Invoke(this.lives);
        OnEnergyChanged?.Invoke(this.energy);
        OnScoreChanged?.Invoke(this.Score);
        UpdateInventoryCount();
    }


    public void Move(int dx, int dy)
    {
        int newX = gridX + dx;
        int newY = gridY + dy;
        
        if (gm == null) return;
        
        if (newX >= 0 && newX < gm.size && newY >= 0 && newY < gm.size)
        {
            Tile targetTile = gm.board[newX, newY];

            if (targetTile.type != TileType.Wall) 
            {
                gridX = newX;
                gridY = newY;
                transform.position = gm.GridToWorld(gridX, gridY);
                
                gm.RevealArea(gridX, gridY, 1);
                
                TryInteract(0, 0); 
            }
            else 
            {
                gm.ShowNotification("¡Choque! Pierdes 1 de energía.", 1.5f);
                UseEnergy(1);
                gm.RevealArea(newX, newY, 1); 
            }
        }
    }
    
    
    public void TryInteract(int dx, int dy)
    {
        int targetX = gridX + dx;
        int targetY = gridY + dy;
        
        if (gm == null || gm.board == null) return;
        
        if (targetX >= 0 && targetX < gm.size && 
            targetY >= 0 && targetY < gm.size)
        {
            Tile targetTile = gm.board[targetX, targetY];
            
            if (targetTile.type == TileType.Exit)
            {
                gm.LevelComplete();
                return;
            }
            if (targetTile.content != null)
            {
                WorldItem wi = targetTile.content;
                
                TreasureData treasure = wi as TreasureData;

                if (treasure != null)
                {
                    treasure.Collect(this); 
                    
                    if (targetTile.content != null && !targetTile.content.gameObject.activeSelf)
                    {
                        targetTile.content = null;
                    }
                }
                else
                {
                    wi.Interact(this);
                    
                    if (wi == null || (wi != null && wi.gameObject == null)) 
                    {
                         targetTile.content = null;
                    }
                }
            }
        }
    }
    

    public void ToggleInventory()
    {
        bool gameIsRunning = Time.timeScale.Equals(1f); 
        bool inventoryShouldOpen = gameIsRunning; 

        Time.timeScale = inventoryShouldOpen ? 0f : 1f;

        OnToggleInventory?.Invoke(inventoryShouldOpen);
    }

    public void UpdateInventoryCount()
    {
        OnInventoryChanged?.Invoke(inventoryItems.Count);
    }
    
    
    public void UseEnergy(int amount)
    {
        energy = Mathf.Max(0, energy - amount);
        OnEnergyChanged?.Invoke(energy);

        if (energy <= 0)
        {
            gm.ShowNotification("¡Sin Energía! Pierdes 1 Vida.", 2f);
            TakeDamage(1); 
            energy = maxEnergy; 
            OnEnergyChanged?.Invoke(energy);
        }
    }
    
    public void TakeDamage(int damage)
    {
        lives = Mathf.Max(0, lives - damage);
        OnLivesChanged?.Invoke(lives);
        
        if (lives <= 0)
        {
            gm.GameOver();
        }
    }
    
    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }
    
    public void AddLife(int amount)
    {
        lives = Mathf.Min(maxLives, lives + amount);
        OnLivesChanged?.Invoke(lives);
    }

    public void AddEnergy(int amount)
    {
        energy = Mathf.Min(maxEnergy, energy + amount);
        OnEnergyChanged?.Invoke(energy);
    }
}