using UnityEngine;
using System.Collections.Generic;

public enum TrustState { Hostile, Neutral, Friendly }

[CreateAssetMenu(menuName = "Faction/Faction Data")]
public class FactionData : ScriptableObject
{
    public string factionName;
    public Sprite factionIcon;
    public float trustLevel = 50f;

    public float hostileThreshold = 20f;
    public float friendlyThreshold = 75f;

    public MissionData defaultMission;
    public MissionChainData missionChain; // Assign in Inspector
    public List<ItemData> preferredItems;

    public TrustState GetTrustState()
    {
        if (trustLevel <= hostileThreshold) return TrustState.Hostile;
        if (trustLevel >= friendlyThreshold) return TrustState.Friendly;
        return TrustState.Neutral;
    }

    // 🆕 New Method to Generate a Request Based on Leadership Style
    public string GenerateRequest(LeadershipStyle style)
    {
        switch (factionName.ToLower())
        {
            case "crows": return GenerateCrowRequest(style);
            case "sparrows": return GenerateSparrowRequest(style);
            case "blue jays": return GenerateBlueJayRequest(style);
            case "doves": return GenerateDoveRequest(style);
            default: return $"{factionName} doesn't have a request yet.";
        }
    }

    private string GenerateCrowRequest(LeadershipStyle style)
    {
        switch (style)
        {
            case LeadershipStyle.Dictator:
                return "The Crows demand control over shiny goods to intimidate rivals.";
            case LeadershipStyle.Utopian:
                return "The Crows ask for a seat in the new council to ensure fairness.";
            case LeadershipStyle.Pragmatist:
                return "The Crows offer protection in exchange for control over scavenging routes.";
            default:
                return "The Crows are undecided.";
        }
    }

    private string GenerateSparrowRequest(LeadershipStyle style)
    {
        switch (style)
        {
            case LeadershipStyle.Dictator:
                return "The Sparrows agree to serve, but want patrols to ensure their safety.";
            case LeadershipStyle.Utopian:
                return "The Sparrows request shared food supplies for the weaker members.";
            case LeadershipStyle.Pragmatist:
                return "The Sparrows want to establish a barter system.";
            default:
                return "The Sparrows need more time.";
        }
    }

    private string GenerateBlueJayRequest(LeadershipStyle style)
    {
        switch (style)
        {
            case LeadershipStyle.Dictator:
                return "The Blue Jays demand extra resources or they'll resist your rule.";
            case LeadershipStyle.Utopian:
                return "The Blue Jays want open trade between all nests.";
            case LeadershipStyle.Pragmatist:
                return "The Blue Jays ask for limited trade privileges and control over commerce.";
            default:
                return "The Blue Jays are watching carefully.";
        }
    }

    private string GenerateDoveRequest(LeadershipStyle style)
    {
        switch (style)
        {
            case LeadershipStyle.Dictator:
                return "The Doves warn they’ll flee if peace is not maintained.";
            case LeadershipStyle.Utopian:
                return "The Doves support your vision and request aid for the elders.";
            case LeadershipStyle.Pragmatist:
                return "The Doves ask for a neutral safe zone.";
            default:
                return "The Doves remain silent.";
        }
    }
}
