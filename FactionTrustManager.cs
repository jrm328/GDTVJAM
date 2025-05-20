using UnityEngine;

public class FactionTrustManager : MonoBehaviour
{
    public static FactionTrustManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ModifyTrust(FactionData faction, float amount)
    {
        float oldTrust = faction.trustLevel;
        faction.trustLevel = Mathf.Clamp(faction.trustLevel + amount, 0f, 100f);

        TrustState newState = faction.GetTrustState();

        // Optional: Trigger logic when crossing thresholds
        if (faction.trustLevel <= faction.hostileThreshold && oldTrust > faction.hostileThreshold)
            OnBecameHostile(faction);
        else if (faction.trustLevel >= faction.friendlyThreshold && oldTrust < faction.friendlyThreshold)
            OnBecameFriendly(faction);

        FactionPanelUI.Instance?.RefreshTrustBar(faction);
    }

    private void OnBecameHostile(FactionData faction)
    {
        Debug.Log($"{faction.factionName} is now Hostile!");
        // Trigger a bad event here
    }

    private void OnBecameFriendly(FactionData faction)
    {
        Debug.Log($"{faction.factionName} is now Friendly!");
        // Trigger a bonus event or mission
    }
}
