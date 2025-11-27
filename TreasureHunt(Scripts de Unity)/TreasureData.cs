
using UnityEngine;

public enum TreasureEffectType 
{
    ScorePoints,
    RestoreEnergy, 
    GainLife,
}

public class TreasureData : WorldItem
{
    [Header("UI")]
    public Sprite inventoryIcon;
    
    [Header("Treasure Settings")]
    public int Value = 100;
    public TreasureEffectType EffectType = TreasureEffectType.ScorePoints;
    public int EffectAmount = 1; 
    public void Collect(PlayerController player)
    {
        if (player.inventoryItems.Count < player.maxInventory)
        {
            player.inventoryItems.Add(this); 
            
            gameObject.SetActive(false); 
            
            GameManager.Instance.ShowNotification($"Tesoro guardado: {EffectType}.", 1.5f);
            
            player.UpdateInventoryCount();
        }
        else
        {
            GameManager.Instance.ShowNotification("¡Inventario lleno!", 1.5f);
        }
    }

    public void ApplyEffect(PlayerController player)
    {
        string notificationMessage = "";
        bool effectApplied = false;
        switch (EffectType)
        {
            case TreasureEffectType.ScorePoints:
                if (EffectAmount > 0)
                {
                    player.AddScore(EffectAmount);
                    notificationMessage = $"Usado: Ganas {EffectAmount} puntos.";
                    effectApplied = true;
                }
                break;
                
            case TreasureEffectType.RestoreEnergy:
                if (EffectAmount > 0)
                {
                    player.AddEnergy(EffectAmount);
                    notificationMessage = $"Usado: Restauras {EffectAmount} de energía.";
                    effectApplied = true;
                }
                break;
                
            case TreasureEffectType.GainLife:
                if (EffectAmount > 0)
                {
                    player.AddLife(EffectAmount);
                    notificationMessage = $"Usado: Ganas {EffectAmount} vida.";
                    effectApplied = true;
                }
                break;
        }

        if (effectApplied && GameManager.Instance != null)
        {
            GameManager.Instance.ShowNotification(notificationMessage, 1.5f);
        }
        
        Destroy(gameObject);
    }

    public override void Interact(PlayerController player)
    {

    }
}