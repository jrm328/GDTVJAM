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
                // ✅ Grab next mission from chain
                MissionData nextMission = MissionTracker.Instance.GetCurrentMission(factionData);

                if (nextMission != null)
                {
                    MissionManager.Instance.StartMission(nextMission);
                    TooltipUI.Instance?.ShowTooltip("Mission Accepted!");
                }
                else
                {
                    TooltipUI.Instance?.ShowTooltip("No more missions from this faction.");
                }

                JournalUI.Instance?.RefreshUI();
            }),

            new DialogueChoice("No way.", () =>
            {
                FactionTrustManager.Instance.ModifyTrust(factionData, -5f);
                JournalUI.Instance?.RefreshUI();
            }),

            new DialogueChoice("Let me think about it.", () => { })
        };

        // ✅ Get active, incomplete mission (if relevant)
        MissionData activeMission = MissionManager.Instance.GetActiveMission();
        bool isMissionTrade = activeMission != null && !activeMission.isCompleted && activeMission.assignedFaction == factionData;
        ItemData tradeItem = GetRelevantTradeItem();
        int owned = tradeItem != null ? InventoryManager.Instance.GetItemCount(tradeItem) : 0;

        if (tradeItem != null)
        {
            string label = $"Trade {tradeItem.itemName}";

            choices.Add(new DialogueChoice(label, () =>
            {
                if (owned <= 0)
                {
                    TooltipUI.Instance?.ShowTooltip($"You need {tradeItem.itemName}!");
                    return;
                }

                if (isMissionTrade)
                {
                    int needed = activeMission.objectiveCount - activeMission.currentProgress;
                    int toGive = Mathf.Min(owned, needed);

                    for (int i = 0; i < toGive; i++)
                    {
                        InventoryManager.Instance.RemoveItem(tradeItem);
                        MissionManager.Instance.RegisterObjectiveCompleted(tradeItem);
                    }

                    TooltipUI.Instance?.ShowTooltip($"Traded {toGive}x {tradeItem.itemName} for mission.");
                }
                else
                {
                    InventoryManager.Instance.RemoveItem(tradeItem);
                    FactionTrustManager.Instance.ModifyTrust(factionData, +5f);
                    TooltipUI.Instance?.ShowTooltip($"Traded 1x {tradeItem.itemName}.");
                }

                JournalUI.Instance?.RefreshUI();
            }));
        }

        DialogueSystem.Instance.ShowDialogue(requestText, factionData.factionIcon, choices);
    }

    private ItemData GetRelevantTradeItem()
    {
        var mission = MissionManager.Instance.GetActiveMission();
        if (mission != null && mission.assignedFaction == factionData)
            return mission.targetItem;

        if (factionData.preferredItems != null && factionData.preferredItems.Count > 0)
            return factionData.preferredItems[0];

        return null;
    }
}
