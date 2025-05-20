using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public GameObject collectiblePrefab; // Assign your CollectibleItem prefab

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartBreadcrumbMission(ItemData item, Vector3 center, FactionData sparrowFaction)
    {
        SpawnCollectibles(item, 3, center);
        FactionZoneManager.Instance.EnableFaction("Sparrows");

        // Dictator path — intimidate first, build trust later
        FactionTrustManager.Instance.ModifyTrust(sparrowFaction, +5f);
    }

    public void StartShinyMission(ItemData item, Vector3 center, FactionData crowFaction)
    {
        SpawnCollectibles(item, 1, center);
        FactionZoneManager.Instance.EnableFaction("Crows");

        // Utopian peace offer
        FactionTrustManager.Instance.ModifyTrust(crowFaction, +10f);
    }

    public void StartWormMission(ItemData worm, ItemData cap, Vector3 center, FactionData blueJayFaction)
    {
        SpawnCollectibles(worm, 2, center);
        SpawnCollectibles(cap, 1, center + Vector3.right * 2);
        FactionZoneManager.Instance.EnableFaction("Blue Jays");

        // Pragmatist initial favor
        FactionTrustManager.Instance.ModifyTrust(blueJayFaction, +5f);
    }

    private void SpawnCollectibles(ItemData item, int count, Vector3 center)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            GameObject obj = Instantiate(collectiblePrefab, center + offset, Quaternion.identity);
            obj.GetComponent<CollectibleItem>().itemData = item;
        }
    }
}
