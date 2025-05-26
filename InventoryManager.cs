using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Data")]
    private Dictionary<ItemData, int> inventory = new();

    [Header("UI Reference")]
    public InventoryUI inventoryUI;

    private void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else Destroy(gameObject);
        RefreshInventoryUI();
    }

    /// <summary>
    /// Adds an item to the inventory and refreshes the UI.
    /// </summary>
    /// <param name="item">The item to add</param>
    public void AddItem(ItemData item)
    {
        if (inventory.ContainsKey(item))
            inventory[item]++;
        else
            inventory[item] = 1;

        RefreshInventoryUI();
    }

    /// <summary>
    /// Refreshes the inventory UI with current items.
    /// </summary>
    public void RefreshInventoryUI()
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning("Inventory UI not assigned in InventoryManager.");
            return;
        }

        // Hide inventory UI if empty
        if (inventory == null || inventory.Count == 0)
        {
            inventoryUI.gameObject.SetActive(false);
        }
        else
        {
            inventoryUI.gameObject.SetActive(true);
            inventoryUI.RefreshUI(inventory);
        }
    }

    /// <summary>
    /// Gets the amount of a specific item currently in inventory.
    /// </summary>
    public int GetItemCount(ItemData item)
    {
        return inventory.ContainsKey(item) ? inventory[item] : 0;
    }

    /// <summary>
    /// Removes one unit of an item from inventory (if it exists).
    /// </summary>
    public bool RemoveItem(ItemData item)
    {
        if (inventory.ContainsKey(item) && inventory[item] > 0)
        {
            inventory[item]--;
            if (inventory[item] == 0)
                inventory.Remove(item);

            RefreshInventoryUI();
            return true;
        }

        return false;
    }
}
