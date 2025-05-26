using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    private MissionData activeMission;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartMission(MissionData mission)
    {
        if (mission == null)
        {
            Debug.LogWarning("❌ Tried to start a null mission.");
            return;
        }

        activeMission = Instantiate(mission);
        activeMission.currentProgress = 0;
        activeMission.isCompleted = false;

        Debug.Log($"🟢 Started mission: {activeMission.missionName}");

        // Spawn items at random zone from GameInitializer
        Transform zone = GameInitializer.Instance?.GetRandomSpawnZone();
        Vector3 spawnPos = zone != null ? zone.position : Vector3.zero;

        SpawnCollectibles(activeMission.targetItem, activeMission.objectiveCount, spawnPos);

        JournalUI.Instance?.RefreshUI();
    }

    public void RegisterObjectiveCompleted(ItemData item)
    {
        if (activeMission == null || activeMission.isCompleted) return;
        if (item != activeMission.targetItem) return;

        activeMission.currentProgress++;
        Debug.Log($"✅ Mission Progress: {activeMission.currentProgress}/{activeMission.objectiveCount}");

        if (activeMission.currentProgress >= activeMission.objectiveCount)
        {
            activeMission.isCompleted = true;
            Debug.Log($"🏆 Mission '{activeMission.missionName}' completed!");

            // Trust reward
            FactionTrustManager.Instance.ModifyTrust(activeMission.assignedFaction, activeMission.trustReward);

            // UI feedback
            TooltipUI.Instance?.ShowTooltip(
                $"Mission Complete!\n+{activeMission.trustReward} Trust with {activeMission.assignedFaction.factionName}"
            );
            JournalUI.Instance?.RefreshUI();

            // Chain progression
            MissionTracker.Instance?.RegisterMissionComplete(activeMission.assignedFaction);

            activeMission = null;

            // Optional: Check for win/lose condition
            if (GameEndingManager.Instance != null && GameEndingManager.Instance.AllChainsResolved())
            {
                GameEndingManager.Instance.EvaluateEnding();
            }
        }
    }

    private void SpawnCollectibles(ItemData item, int count, Vector3 center)
    {
        if (item == null || item.worldPrefab == null)
        {
            Debug.LogError($"❌ Cannot spawn collectibles: '{item?.itemName ?? "NULL"}' is missing or has no prefab.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            GameObject obj = Instantiate(item.worldPrefab, center + offset, Quaternion.identity);

            CollectibleItem collectible = obj.GetComponent<CollectibleItem>();
            if (collectible != null)
                collectible.itemData = item;
        }
    }

    public MissionData GetActiveMission() => activeMission;

    public void ClearMission()
    {
        activeMission = null;
        JournalUI.Instance?.RefreshUI();
    }
}
