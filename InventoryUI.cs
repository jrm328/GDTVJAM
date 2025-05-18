using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;

    private Dictionary<ItemData, GameObject> slotDict = new();

    public void RefreshUI(Dictionary<ItemData, int> inventory)
    {
        // 🧹 Step 1: Remove slots for items no longer in inventory
        var keysToRemove = new List<ItemData>();
        foreach (var kvp in slotDict)
        {
            if (!inventory.ContainsKey(kvp.Key))
            {
                Destroy(kvp.Value);
                keysToRemove.Add(kvp.Key);
            }
        }
        foreach (var key in keysToRemove)
        {
            slotDict.Remove(key);
        }

        // 🔁 Step 2: Create or update visible slots
        foreach (var item in inventory)
        {
            if (!slotDict.ContainsKey(item.Key))
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.icon;
                slotDict[item.Key] = slot;
            }

            slotDict[item.Key].transform.GetChild(1).GetComponent<TMP_Text>().text = item.Value.ToString();
        }
    }
}
