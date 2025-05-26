using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JournalUI : MonoBehaviour
{
    [Header("Mission Section")]
    public GameObject missionPanel; // Toggles the mission panel on/off
    public TMP_Text missionTitle;
    public TMP_Text missionDesc;
    public TMP_Text missionProgress;
    public Image missionFactionIcon;

    public static JournalUI Instance;

    [Header("Trust Section")]
    public List<FactionTrustUI> factionTrustDisplays;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var activeMission = MissionManager.Instance.GetActiveMission();

        if (activeMission != null && !activeMission.isCompleted)
        {
            missionPanel.SetActive(true);

            missionTitle.text = activeMission.missionName;
            missionDesc.text = activeMission.description;
            missionProgress.text = $"{activeMission.currentProgress} / {activeMission.objectiveCount}";
            missionFactionIcon.sprite = activeMission.assignedFaction.factionIcon;
        }
        else
        {
            missionPanel.SetActive(false);

            missionTitle.text = "";
            missionDesc.text = "";
            missionProgress.text = "";
            missionFactionIcon.sprite = null;
        }

        foreach (var display in factionTrustDisplays)
        {
            display.UpdateTrust();
        }
    }
}
