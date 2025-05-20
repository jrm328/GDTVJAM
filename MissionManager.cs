using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

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
        FactionZoneManager.Instance.EnableFaction("Sparrows");
        FactionTrustManager.Instance.ModifyTrust(sparrowFaction, +5f);
    }

    public void StartShinyMission(ItemData item, Vector3 center, FactionData crowFaction)
    {
        SpawnCollectibles(item, 1, center);
        FactionZoneManager.Instance.EnableFaction("Crows");
        FactionTrustManager.Instance.ModifyTrust(crowFaction, +10f);
    }

    public void StartWormMission(ItemData worm, ItemData cap, Vector3 center, FactionData blueJayFaction)
    {
        SpawnCollectibles(worm, 2, center);
        SpawnCollectibles(cap, 1, center + Vector3.right * 2);
        FactionZoneManager.Instance.EnableFaction("Blue Jays");
        FactionTrustManager.Instance.ModifyTrust(blueJayFaction, +5f);
    }

    private void SpawnCollectibles(ItemData item, int count, Vector3 center)
    {
        if (item == null)
        {
            Debug.LogError("❌ Attempted to spawn collectibles with a null ItemData.");
            return;
        }

        if (item.worldPrefab == null)
        {
            Debug.LogError($"❌ Item '{item.itemName}' has no worldPrefab assigned.");
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
}
