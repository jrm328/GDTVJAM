using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Missions/Mission Chain")]
public class MissionChainData : ScriptableObject
{
    public FactionData faction;
    public LeadershipStyle requiredStyle; // Optional filter, if you want chains by player path
    public List<MissionData> missions;    // Ordered list of missions
}
