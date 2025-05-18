using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public InventoryUI inventoryUI;
    public FactionPanelUI factionPanelUI;

    [Header("Faction Settings")]
    public List<FactionData> allFactions;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Initialize inventory display at start
        InventoryManager.Instance.RefreshInventoryUI();
    }

    public void ModifyTrust(FactionData faction, float amount)
    {
        float adjustedAmount = amount;

        if (amount > 0)
            adjustedAmount *= faction.trustGainMultiplier;
        else
            adjustedAmount *= faction.trustLossMultiplier;

        faction.trustLevel = Mathf.Clamp(faction.trustLevel + adjustedAmount, 0, 100);
        factionPanelUI.RefreshTrustBar(faction);

        Debug.Log($"Modified trust with {faction.factionName}: {adjustedAmount}. New trust: {faction.trustLevel}");
    }

    public void OpenFactionPanel(string factionName)
    {
        FactionData faction = allFactions.Find(f => f.factionName == factionName);
        if (faction != null)
        {
            factionPanelUI.Open(faction);
        }
        else
        {
            Debug.LogWarning($"Faction '{factionName}' not found in GameManager.");
        }
    }
}
