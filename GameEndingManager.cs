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

    private void TriggerUnityEnding()
    {
        Debug.Log("Unity Ending triggered!");
        // TODO: show end screen, play animation, etc.
    }

    private void TriggerCollapseEnding()
    {
        Debug.Log("Collapse Ending triggered!");
    }

    private void TriggerIsolationEnding()
    {
        Debug.Log("Isolation Ending triggered!");
    }

    private void TriggerNeutralEnding()
    {
        Debug.Log("Neutral Ending triggered!");
    }

}
