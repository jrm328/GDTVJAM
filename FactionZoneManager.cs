using UnityEngine;

public class FactionZoneManager : MonoBehaviour
{

    public GameObject sparrowZone, crowZone, blueJayZone, doveZone;

    public static FactionZoneManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Add this line

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableFaction(string name)
    {
        Debug.Log($"[FactionZoneManager] Enabling zone: {name}");

        switch (name)
        {
            case "Sparrows":
                if (sparrowZone == null) Debug.LogError("Sparrow Zone is not assigned!");
                sparrowZone?.SetActive(true);
                break;
            case "Crows":
                if (crowZone == null) Debug.LogError("Crow Zone is not assigned!");
                crowZone?.SetActive(true);
                break;
            case "Blue Jays":
                if (blueJayZone == null) Debug.LogError("Blue Jay Zone is not assigned!");
                blueJayZone?.SetActive(true);
                break;
            case "Doves":
                if (doveZone == null) Debug.LogError("Dove Zone is not assigned!");
                doveZone?.SetActive(true);
                break;
        }
    }
}
