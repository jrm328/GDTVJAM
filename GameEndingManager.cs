using UnityEngine;

public class GameEndingManager : MonoBehaviour
{
    public FactionData[] allFactions;
    public FactionData doves;

    public void CheckForEnding()
    {
        int friendlyCount = 0;
        int hostileCount = 0;

        foreach (var faction in allFactions)
        {
            var state = faction.GetTrustState();
            if (state == TrustState.Friendly) friendlyCount++;
            if (state == TrustState.Hostile) hostileCount++;
        }

        if (friendlyCount >= 3 && doves.GetTrustState() == TrustState.Friendly)
            TriggerUnityEnding();
        else if (hostileCount >= 3)
            TriggerCollapseEnding();
        else if (doves.GetTrustState() == TrustState.Hostile)
            TriggerIsolationEnding();
        else
            TriggerNeutralEnding();
    }
}
