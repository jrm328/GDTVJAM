using UnityEngine;
using System.Collections.Generic;

public class MissionTracker : MonoBehaviour
{
    public static MissionTracker Instance;

    private Dictionary<FactionData, int> progress = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterMissionComplete(FactionData faction)
    {
        if (faction == null || faction.missionChain == null)
        {
            Debug.LogWarning("⚠️ No mission chain found for faction.");
            return;
        }

        if (!progress.ContainsKey(faction))
            progress[faction] = 0;

        progress[faction]++;

        if (progress[faction] < faction.missionChain.missions.Count)
        {
            MissionData nextMission = faction.missionChain.missions[progress[faction]];
            Debug.Log($"➡️ Advancing mission chain for {faction.factionName}: {nextMission.missionName}");
            MissionManager.Instance.StartMission(nextMission);
        }
        else
        {
            Debug.Log($"✅ Mission chain complete for {faction.factionName}.");
        }
    }

    public MissionData GetCurrentMission(FactionData faction)
    {
        if (faction == null || faction.missionChain == null)
            return null;

        int step = GetCurrentStep(faction);
        if (step < faction.missionChain.missions.Count)
            return faction.missionChain.missions[step];

        return null;
    }

    public int GetCurrentStep(FactionData faction)
    {
        if (!progress.TryGetValue(faction, out int index))
            return 0;

        return index;
    }

    public bool IsChainComplete(MissionChainData chain)
    {
        foreach (var pair in progress)
        {
            if (pair.Key.missionChain == chain &&
                pair.Value < chain.missions.Count)
                return false;
        }

        return true;
    }
}
