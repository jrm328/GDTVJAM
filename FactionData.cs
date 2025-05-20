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

    public TrustState GetTrustState()
    {
        if (trustLevel <= hostileThreshold) return TrustState.Hostile;
        if (trustLevel >= friendlyThreshold) return TrustState.Friendly;
        return TrustState.Neutral;
    }
}

