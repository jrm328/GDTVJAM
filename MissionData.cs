using UnityEngine;

[CreateAssetMenu(menuName = "Peckonomics/Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public string description;
    public FactionData assignedFaction;
    public int objectiveCount;
    public ItemData targetItem;

    [HideInInspector] public int currentProgress;
    [HideInInspector] public bool isCompleted;
}
