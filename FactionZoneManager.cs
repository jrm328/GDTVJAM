using UnityEngine;

public class FactionZoneManager : MonoBehaviour
{
    public static FactionZoneManager Instance;

    public GameObject sparrowZone, crowZone, blueJayZone;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EnableFaction(string name)
    {
        switch (name)
        {
            case "Sparrows":
                sparrowZone.SetActive(true); break;
            case "Crows":
                crowZone.SetActive(true); break;
            case "Blue Jays":
                blueJayZone.SetActive(true); break;
        }
    }
}
