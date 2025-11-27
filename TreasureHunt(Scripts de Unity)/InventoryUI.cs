using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject slotContainer; 
    public GameObject itemSlotPrefab;

    private PlayerController player;
    private List<GameObject> activeSlots = new List<GameObject>();

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        if (player == null) 
        {
            Debug.LogError("PlayerController no encontrado en la escena.");
            return;
        }
        PlayerController.OnToggleInventory += ToggleInventoryPanel;
        
    
        gameObject.SetActive(false);

     
        SetupSlots();
    }
  
    private void SetupSlots()
    {
        if (player == null) return;

  
        foreach (Transform child in slotContainer.transform)
        {
            Destroy(child.gameObject);
        }
        activeSlots.Clear();

     
        for (int i = 0; i < player.maxInventory; i++)
        {
            GameObject slotInstance = Instantiate(itemSlotPrefab, slotContainer.transform);
            activeSlots.Add(slotInstance);
        }
    }

        public void RefreshUI()
    {
        if (player == null) return;

       
        for (int i = 0; i < activeSlots.Count; i++)
        {
            InventorySlotUI slot = activeSlots[i].GetComponent<InventorySlotUI>();
            
            if (i < player.inventoryItems.Count)
            {
               
                WorldItem item = player.inventoryItems[i];
                slot.Setup(item, player, this);
                activeSlots[i].SetActive(true);
            }
            else
            {
          
                slot.associatedItem = null;
                activeSlots[i].SetActive(false);
            }
        }
        
        player.UpdateInventoryCount(); 
    }

      private void ToggleInventoryPanel(bool shouldOpen)
    {
        gameObject.SetActive(shouldOpen);

    
        if (shouldOpen)
        {
            RefreshUI();
        }
    }
    
    void OnDestroy()
    {
        PlayerController.OnToggleInventory -= ToggleInventoryPanel;
    }
}