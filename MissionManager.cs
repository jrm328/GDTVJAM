using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    private MissionData activeMission;

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

    public void StartBreadcrumbMission(ItemData item, Vector3 center, FactionData sparrowFaction)
    {
        SpawnCollectibles(item, 3, center);
        FactionTrustManager.Instance.ModifyTrust(sparrowFaction, +5f);
        JournalUI.Instance.RefreshUI();
    }

    public void StartShinyMission(ItemData item, Vector3 center, FactionData crowFaction)
    {
        SpawnCollectibles(item, 1, center);
        FactionTrustManager.Instance.ModifyTrust(crowFaction, +10f);
        JournalUI.Instance.RefreshUI();
    }

    public void StartWormMission(ItemData worm, ItemData cap, Vector3 center, FactionData blueJayFaction)
    {
        SpawnCollectibles(worm, 2, center);
        SpawnCollectibles(cap, 3, center + Vector3.right * 2);
        FactionTrustManager.Instance.ModifyTrust(blueJayFaction, +5f);
        JournalUI.Instance.RefreshUI();

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
            {
                collectible.itemData = item;
            }
            else
            {
                Debug.LogWarning($"⚠️ Spawned prefab '{obj.name}' is missing a CollectibleItem script.");
            }
        }
    }

    public void StartMission(MissionData mission)
    {
        activeMission = Instantiate(mission);
        activeMission.currentProgress = 0;
        activeMission.isCompleted = false;

        Debug.Log($"🟢 Started mission: {activeMission.missionName}");
        SpawnCollectibles(activeMission.targetItem, activeMission.objectiveCount, Vector3.zero); // Replace with accurate spawn center
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
            FactionTrustManager.Instance.ModifyTrust(activeMission.assignedFaction, +10f);
        }
    }

    // 🆕 This is the only thing you needed to add:
    public MissionData GetActiveMission()
    {
        return activeMission;
    }
}
