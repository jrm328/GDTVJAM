using UnityEngine;

public class FactionTrustManager : MonoBehaviour
{
    public static FactionTrustManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ModifyTrust(FactionData faction, float amount)
    {
        Debug.Log($"[ModifyTrust] Faction: {faction?.factionName ?? "null"}");
        Debug.Log($"[ModifyTrust] GameManager.Instance: {(GameManager.Instance == null ? "null" : "valid")}");
        Debug.Log($"[ModifyTrust] GameManager.Instance.factionPanelUI: {(GameManager.Instance?.factionPanelUI == null ? "null" : "valid")}");

        float oldTrust = faction.trustLevel;
        faction.trustLevel = Mathf.Clamp(faction.trustLevel + amount, 0f, 100f);

        TrustState newState = faction.GetTrustState();

        // Optional: trigger milestone events
        if (faction.trustLevel <= faction.hostileThreshold && oldTrust > faction.hostileThreshold)
            OnBecameHostile(faction);
        else if (faction.trustLevel >= faction.friendlyThreshold && oldTrust < faction.friendlyThreshold)
            OnBecameFriendly(faction);

        // ✅ Refresh other UI that cares about trust
        GameManager.Instance.factionPanelUI?.RefreshTrustBar(faction);

        // ✅ 🆕 Refresh journal UI if open
        if (JournalUI.Instance != null)
            JournalUI.Instance.RefreshUI();
    }

    private void OnBecameHostile(FactionData faction)
    {
        Debug.Log($"{faction.factionName} is now Hostile!");
        // Add event triggers here if needed
    }

    private void OnBecameFriendly(FactionData faction)
    {
        Debug.Log($"{faction.factionName} is now Friendly!");
        // Add event triggers here if needed
    }
}
