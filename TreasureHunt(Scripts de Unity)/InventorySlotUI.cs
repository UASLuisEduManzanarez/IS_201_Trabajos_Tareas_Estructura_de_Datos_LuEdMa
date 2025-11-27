using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage; 
    
    public Button useButton; 
    
    public Button dropButton; 

    [HideInInspector] public WorldItem associatedItem;
    private PlayerController player;
    private InventoryUI inventoryUI;

    public void Setup(WorldItem item, PlayerController pc, InventoryUI ui)
    {
        associatedItem = item;
        player = pc;
        inventoryUI = ui;

        if (associatedItem != null)
        {
            gameObject.SetActive(true);
            
            
            TreasureData treasure = associatedItem as TreasureData;
            
            if (treasure != null && treasure.inventoryIcon != null)
            {
                iconImage.sprite = treasure.inventoryIcon;
                iconImage.color = Color.white; 
            }
            else
            {
                
                iconImage.sprite = null; 
                iconImage.color = Color.gray; 
            }

          
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(OnUseItem);
            
          
            if (dropButton != null)
            {
                dropButton.gameObject.SetActive(true);
                dropButton.onClick.RemoveAllListeners();
                dropButton.onClick.AddListener(OnDropItem);
            }
        }
        else
        {
        
            gameObject.SetActive(false);
            iconImage.sprite = null;
        }
    }

    public void OnUseItem()
    {
        if (associatedItem == null) return;
        
       
        TreasureData treasure = associatedItem as TreasureData;
        if (treasure == null)
        {
            Debug.LogWarning("Intentando usar un WorldItem que no es TreasureData.");
            return;
        }
        
        
        treasure.ApplyEffect(player);

        
        Destroy(associatedItem.gameObject);
        
       
        player.inventoryItems.Remove(associatedItem);
        
        
        inventoryUI.RefreshUI();
    }

    
    public void OnDropItem()
    {
        if (associatedItem == null) return;
        
     
        GameManager.Instance.ShowNotification($"Has soltado {associatedItem.gameObject.name}.", 1.5f);
        
     
        Destroy(associatedItem.gameObject); 

     
        player.inventoryItems.Remove(associatedItem);
        
    
        inventoryUI.RefreshUI();
        
      
    }
}