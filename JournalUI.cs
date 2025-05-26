using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JournalUI : MonoBehaviour
{
    [Header("Mission Section")]
    public GameObject missionPanel; // ← NEW: The whole panel to toggle
    public TMP_Text missionTitle;
    public TMP_Text missionDesc;
    public TMP_Text missionProgress;
    public Image missionFactionIcon;
    public static JournalUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Trust Section")]
    public List<FactionTrustUI> factionTrustDisplays;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var activeMission = MissionManager.Instance.GetActiveMission();

        if (activeMission != null)
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
