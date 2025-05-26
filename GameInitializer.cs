using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance;

    [Header("Faction Data References")]
    public FactionData sparrows;
    public FactionData crows;
    public FactionData blueJays;
    public FactionData doves;

    [Header("Collectible Items")]
    public ItemData breadcrumbs;
    public ItemData shinyThing;
    public ItemData seeds;

    [Header("Spawn Settings")]
    public Transform playerSpawnPoint;

    [Header("Game State Prefab (Optional)")]
    [Tooltip("Only needed if GameStateManager might not already be in the scene.")]
    [SerializeField] private GameStateManager gameStatePrefab;

    [Header("Item Spawn Zones")]
    public Transform[] spawnZones = new Transform[4]; // Assign in Inspector

    private void Awake()
    {
        // ✅ Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ✅ Ensure GameStateManager exists
        if (GameStateManager.Instance == null && gameStatePrefab != null)
        {
            Instantiate(gameStatePrefab);
            Debug.Log("[GameInitializer] Instantiated GameStateManager from prefab.");
        }
    }

    public void ApplyLeadershipStyle(LeadershipStyle style)
    {
        InitializeFactions(style);

        switch (style)
        {
            case LeadershipStyle.Dictator:
                SpawnCollectibles(breadcrumbs, 3, spawnZones[0].position);
                MissionData sparrowMission = MissionTracker.Instance.GetCurrentMission(sparrows);
                MissionManager.Instance.StartMission(sparrowMission);
                break;

            case LeadershipStyle.Utopian:
                SpawnCollectibles(shinyThing, 1, spawnZones[1].position);
                MissionData crowMission = MissionTracker.Instance.GetCurrentMission(crows);
                MissionManager.Instance.StartMission(crowMission);
                break;

            case LeadershipStyle.Pragmatist:
                SpawnCollectibles(seeds, 5, spawnZones[2].position);
                MissionData blueJayMission = MissionTracker.Instance.GetCurrentMission(blueJays);
                MissionManager.Instance.StartMission(blueJayMission);
                break;
        }

        Debug.Log("[GameInitializer] Initial items spawned for leadership style: " + style);
    }


    private void InitializeFactions(LeadershipStyle style)
    {
        sparrows.trustLevel = 40;
        sparrows.hostileThreshold = 25;
        sparrows.friendlyThreshold = 70;

        crows.trustLevel = 30;
        crows.hostileThreshold = 35;
        crows.friendlyThreshold = 80;

        blueJays.trustLevel = 50;
        blueJays.hostileThreshold = 20;
        blueJays.friendlyThreshold = 65;

        doves.trustLevel = 60;
        doves.hostileThreshold = 15;
        doves.friendlyThreshold = 75;

        switch (style)
        {
            case LeadershipStyle.Dictator:
                doves.trustLevel -= 15;
                crows.trustLevel += 10;
                sparrows.trustLevel -= 5;
                break;

            case LeadershipStyle.Utopian:
                doves.trustLevel += 10;
                blueJays.trustLevel += 5;
                crows.trustLevel -= 5;
                break;

            case LeadershipStyle.Pragmatist:
                blueJays.trustLevel += 10;
                sparrows.trustLevel += 5;
                doves.trustLevel += 5;
                break;
        }

        Debug.Log("[GameInitializer] Faction trust levels set based on leadership style: " + style);
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
            Vector3 offset = new Vector3(Random.Range(-1.5f, 1.5f), 5f, Random.Range(-1.5f, 1.5f)); // start above
            Vector3 rayOrigin = center + offset;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Ground")))
            {
                GameObject obj = Instantiate(item.worldPrefab, hit.point + Vector3.up * 0.1f, Quaternion.identity);

                CollectibleItem collectible = obj.GetComponent<CollectibleItem>();
                if (collectible != null)
                    collectible.itemData = item;
            }
            else
            {
                Debug.LogWarning($"⚠️ Spawn failed — no ground found beneath {rayOrigin}");
            }
        }
    }


    public Transform GetRandomSpawnZone()
    {
        if (spawnZones == null || spawnZones.Length == 0)
        {
            Debug.LogWarning("⚠️ No spawn zones set in GameInitializer.");
            return null;
        }

        return spawnZones[Random.Range(0, spawnZones.Length)];
    }
}
