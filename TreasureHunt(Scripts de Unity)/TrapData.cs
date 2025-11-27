using UnityEngine;

public class TrapData : WorldItem 
{
    public enum TrapEffectType { DamageLife, DecreaseEnergy }

    [Header("Trap Settings")]
    public TrapEffectType EffectType = TrapEffectType.DamageLife;
    public int EffectAmount = 1;

    public override void Interact(PlayerController player)
    {
        string message = "";

        if (EffectType == TrapEffectType.DamageLife)
        {
            player.TakeDamage(EffectAmount); 
            message = $"¡Trampa! Pierdes {EffectAmount} vida.";
        }
        else if (EffectType == TrapEffectType.DecreaseEnergy)
        {
            player.UseEnergy(EffectAmount);
            message = $"¡Trampa! Pierdes {EffectAmount} de energía.";
        }
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowNotification(message, 1.5f);
        }
        
        base.Interact(player); 
    }
}