using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Attempt to trade an item with a faction.
    /// </summary>
    public void OfferItemToFaction(FactionData faction, ItemData item)
    {
        if (faction == null || item == null)
        {
            Debug.LogError("TradeManager → faction or item is null.");
            return;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("TradeManager → InventoryManager.Instance is null.");
            return;
        }

        int itemCount = InventoryManager.Instance.GetItemCount(item);
        if (itemCount <= 0)
        {
            TooltipUI.Instance?.ShowTooltip($"You don’t have any {item.itemName}s to trade.");
            return;
        }

        bool isPreferred = faction.preferredItems.Contains(item);
        float trustChange = isPreferred ? +15f : +5f;

        // Remove the item
        InventoryManager.Instance.RemoveItem(item);

        // Adjust faction trust
        FactionTrustManager.Instance.ModifyTrust(faction, trustChange);

        // Feedback
        string message = isPreferred
            ? $"{faction.factionName} are impressed by your {item.itemName}!"
            : $"{faction.factionName} accept your {item.itemName}.";

        TooltipUI.Instance?.ShowTooltip(message);
        Debug.Log(message);
    }
}
