using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;

    private Dictionary<ItemData, GameObject> slotDict = new();

    public void RefreshUI(Dictionary<ItemData, int> inventory)
    {
        foreach (var item in inventory)
        {
            if (!slotDict.ContainsKey(item.Key))
            {
                var slot = Instantiate(slotPrefab, slotParent);
                slot.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.icon;
                slotDict[item.Key] = slot;
            }

            slotDict[item.Key].transform.GetChild(1).GetComponent<Text>().text = item.Value.ToString();
        }
    }
}
