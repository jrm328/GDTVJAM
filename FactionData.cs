using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Peckonomics/Faction")]
public class FactionData : ScriptableObject
{
    public string factionName;
    public float trustLevel; // 0–100
    public List<ItemData> preferredItems;
}
