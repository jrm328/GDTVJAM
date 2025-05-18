using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Peckonomics/Faction")]
public class FactionData : ScriptableObject
{
    public string factionName;
    [Range(0, 100)] public float trustLevel = 50;

    public float trustGainMultiplier = 1f;   // e.g., Crows gain less trust
    public float trustLossMultiplier = 1f;

    public Sprite factionPortrait;
    public List<ItemData> preferredItems;
}
