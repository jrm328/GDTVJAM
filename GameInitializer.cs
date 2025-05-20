using UnityEngine;

public enum PlayerArchetype { Dictator, Utopian, Pragmatist }

public class GameInitializer : MonoBehaviour
{
    [Header("Faction Data References")]
    public FactionData sparrows;
    public FactionData crows;
    public FactionData blueJays;
    public FactionData doves;

    [Header("Collectible Items")]
    public ItemData breadcrumbs;
    public ItemData shinyThing;
    public ItemData worm;
    public ItemData bottleCap;

    [Header("Spawn Settings")]
    public Transform playerSpawnPoint;

    private void Awake()
    {
        PlayerArchetype playerArchetype = GameState.playerChoice;
        InitializeFactions(playerArchetype);
    }

    private void InitializeFactions(PlayerArchetype playerArchetype)
    {
        // 🪶 Sparrows
        sparrows.trustLevel = 40;
        sparrows.hostileThreshold = 25;
        sparrows.friendlyThreshold = 70;

        // 🐦 Crows
        crows.trustLevel = 30;
        crows.hostileThreshold = 35;
        crows.friendlyThreshold = 80;

        // 🐤 Blue Jays
        blueJays.trustLevel = 50;
        blueJays.hostileThreshold = 20;
        blueJays.friendlyThreshold = 65;

        // 🕊️ Doves
        doves.trustLevel = 60;
        doves.hostileThreshold = 15;
        doves.friendlyThreshold = 75;

        Vector3 spawnCenter = playerSpawnPoint.position;

        switch (playerArchetype)
        {
            case PlayerArchetype.Dictator:
                doves.trustLevel -= 15;
                crows.trustLevel += 10;
                sparrows.trustLevel -= 5;

                MissionManager.Instance.StartBreadcrumbMission(breadcrumbs, spawnCenter, sparrows);
                break;

            case PlayerArchetype.Utopian:
                doves.trustLevel += 10;
                blueJays.trustLevel += 5;
                crows.trustLevel -= 5;

                MissionManager.Instance.StartShinyMission(shinyThing, spawnCenter, crows);
                break;

            case PlayerArchetype.Pragmatist:
                blueJays.trustLevel += 10;
                sparrows.trustLevel += 5;
                doves.trustLevel += 5;

                MissionManager.Instance.StartWormMission(worm, bottleCap, spawnCenter, blueJays);
                break;
        }

        Debug.Log("[GameInitializer] Faction trust levels set based on player choice: " + playerArchetype);
    }
}
