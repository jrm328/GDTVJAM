using UnityEngine;
using System.Collections.Generic;

public class FactionInteraction : MonoBehaviour
{
    [Header("Faction Setup")]
    public FactionData factionData;

    private void Awake()
    {
        if (factionData == null)
        {
            Debug.LogWarning($"❌ FactionInteraction on '{gameObject.name}' has no FactionData assigned.");
        }
    }

    public void Interact()
    {
        if (factionData == null) return;

        string requestText = factionData.GenerateRequest(GameStateManager.Instance.PlayerLeadershipStyle);

        var choices = new List<DialogueChoice>
        {
            new DialogueChoice("I’ll do it.", () =>
            {
                if (factionData.defaultMission != null)
                {
                    MissionManager.Instance.StartMission(factionData.defaultMission);
                }
            }),
            new DialogueChoice("No way.", () =>
            {
                FactionTrustManager.Instance.ModifyTrust(factionData, -5f);
                if (JournalUI.Instance != null)
                    JournalUI.Instance.RefreshUI();
            }),
            new DialogueChoice("Let me think about it.", () => {})
        };

        // ✅ Add Trade Option BEFORE showing the dialogue
        ItemData tradeItem = GetRelevantTradeItem();
        int owned = tradeItem != null ? InventoryManager.Instance.GetItemCount(tradeItem) : 0;

        if (tradeItem != null)
        {
            string label = owned > 0
                ? $"Trade {owned}x {tradeItem.itemName}"
                : $"You need {tradeItem.itemName}";

            choices.Add(new DialogueChoice(label, () =>
            {
                if (owned > 0)
                {
                    InventoryManager.Instance.RemoveItem(tradeItem);
                    FactionTrustManager.Instance.ModifyTrust(factionData, +5f);
                    Debug.Log($"🟢 Traded 1x {tradeItem.itemName} to {factionData.factionName}");

                    if (JournalUI.Instance != null)
                        JournalUI.Instance.RefreshUI();
                }
            }));
        }

        // ✅ Now show all dialogue choices (including trade)
        DialogueSystem.Instance.ShowDialogue(requestText, factionData.factionIcon, choices);
    }

    private ItemData GetRelevantTradeItem()
    {
        // Priority 1: active mission item
        var mission = MissionManager.Instance.GetActiveMission();
        if (mission != null && mission.assignedFaction == factionData)
        {
            return mission.targetItem;
        }

        // Priority 2: first preferred item
        if (factionData.preferredItems != null && factionData.preferredItems.Count > 0)
        {
            return factionData.preferredItems[0];
        }

        return null;
    }
}
