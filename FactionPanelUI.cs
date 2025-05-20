using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FactionPanelUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text factionNameText;
    public Slider trustSlider;
    public TMP_Text trustValueText;
    public GameObject tradeSection;
    public Button tradeButton;
    public GameObject firstSelectableUIElement; // Assign this in the Inspector

    [Header("Trade Item")]
    public ItemData testTradeItem; // You can hook this up in Inspector or build a UI selector later

    private FactionData currentFaction;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Cancel.performed += OnCancelPressed;
    }

    private void OnDisable()
    {
        inputActions.UI.Cancel.performed -= OnCancelPressed;
        inputActions.UI.Disable();
    }

    public void Open(FactionData faction)
    {
        currentFaction = faction;
        factionNameText.text = faction.factionName;
        UpdateTrustDisplay();
        tradeSection.SetActive(true); // Show trade options
        gameObject.SetActive(true);

        // Focus first UI element for controller navigation
        if (firstSelectableUIElement != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectableUIElement);
        }

        UpdateTradeButtonInteractable();
    }

    public void Close()
    {
        currentFaction = null;
        gameObject.SetActive(false);

        // Optionally clear selected object
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void RefreshTrustBar(FactionData faction)
    {
        trustSlider.value = faction.trustLevel / 100f;
        trustValueText.text = Mathf.RoundToInt(faction.trustLevel).ToString();

        switch (faction.GetTrustState())
        {
            case TrustState.Hostile:
                trustSlider.fillRect.GetComponent<Image>().color = Color.red;
                break;
            case TrustState.Neutral:
                trustSlider.fillRect.GetComponent<Image>().color = Color.yellow;
                break;
            case TrustState.Friendly:
                trustSlider.fillRect.GetComponent<Image>().color = Color.green;
                break;
        }
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

    private void OnCancelPressed(InputAction.CallbackContext ctx)
    {
        Close();
    }
}
