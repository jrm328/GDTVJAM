using UnityEngine;

[CreateAssetMenu(menuName = "Peckonomics/Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public string description;
    public FactionData assignedFaction;
    public int objectiveCount;
    public ItemData targetItem;
    public int trustReward;
    public LeadershipStyle requiredStyle = LeadershipStyle.None; // Optional lock
    public float minTrustRequired = 0f;  // Optional gating by trust
    public float maxTrustAllowed = 100f; // Optional gating by trust ceiling
    [HideInInspector] public int currentProgress;
    [HideInInspector] public bool isCompleted;
}
