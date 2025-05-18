using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactionPanelUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text factionNameText;
    public Slider trustSlider;
    public TMP_Text trustValueText;
    public GameObject tradeSection;
    public Button tradeButton;


    [Header("Trade Item")]
    public ItemData testTradeItem; // You can hook this up in Inspector or build a UI selector later

    private FactionData currentFaction;

    public void Open(FactionData faction)
    {
        currentFaction = faction;
        factionNameText.text = faction.factionName;
        UpdateTrustDisplay();
        tradeSection.SetActive(true); // Show trade button or options
        gameObject.SetActive(true);
        UpdateTradeButtonInteractable();
    }

    public void Close()
    {
        currentFaction = null;
        gameObject.SetActive(false);
    }

    public void RefreshTrustBar(FactionData faction)
    {
        if (currentFaction != faction) return;
        UpdateTrustDisplay();
    }

    private void UpdateTradeButtonInteractable()
    {
        bool hasItem = InventoryManager.Instance.GetItemCount(testTradeItem) > 0;
        tradeButton.interactable = hasItem;

        if (!hasItem)
        {
            TooltipUI.Instance?.ShowTooltip($"You don't have any {testTradeItem.itemName}s to trade!");
        }
    }

    private void UpdateTrustDisplay()
    {
        trustSlider.value = currentFaction.trustLevel / 100f;
        trustValueText.text = Mathf.RoundToInt(currentFaction.trustLevel).ToString();
    }

    public void OnTradeButtonClicked()
    {
        if (currentFaction == null || testTradeItem == null)
        {
            TooltipUI.Instance.ShowTooltip("No faction or item selected.");
            return;
        }

        TradeManager.Instance.OfferItemToFaction(currentFaction, testTradeItem);
        UpdateTrustDisplay(); // Refresh immediately after trade
        UpdateTradeButtonInteractable(); // Update button state
    }
}
