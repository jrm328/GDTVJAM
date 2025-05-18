using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemData);
            TooltipUI.Instance?.ShowTooltip($"Collected {itemData.itemName}!");
            Destroy(gameObject);
        }
    }
}
