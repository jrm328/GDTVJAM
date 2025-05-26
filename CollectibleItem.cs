using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Collected {itemData.itemName}");
            InventoryManager.Instance.AddItem(itemData);
            MissionManager.Instance.RegisterObjectiveCompleted(itemData);
            TooltipUI.Instance?.ShowTooltip($"Collected {itemData.itemName}!");
            Destroy(gameObject);
        }
    }
}
