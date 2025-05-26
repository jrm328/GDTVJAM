using UnityEngine;
using System.Collections.Generic;

public class GameEndingManager : MonoBehaviour
{
    public static GameEndingManager Instance;

    [Header("Faction References")]
    public FactionData sparrows;
    public FactionData crows;
    public FactionData blueJays;
    public FactionData doves;

    [Header("Pigeon Ending Portraits")]
    public Sprite utopianFace;
    public Sprite dictatorFace;
    public Sprite pragmatistFace;
    public Sprite isolationFace;
    public Sprite revoltFace;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EvaluateEnding()
    {
        LeadershipStyle style = GameStateManager.Instance.PlayerLeadershipStyle;

        float sparrowTrust = sparrows.trustLevel;
        float crowTrust = crows.trustLevel;
        float blueJayTrust = blueJays.trustLevel;
        float doveTrust = doves.trustLevel;

        int hostileCount = 0;
        int highTrustCount = 0;
        int midTrustCount = 0;
        int lowTrustCount = 0;

        FactionData[] factions = { sparrows, crows, blueJays, doves };

        foreach (var faction in factions)
        {
            float trust = faction.trustLevel;

            if (trust < faction.hostileThreshold) hostileCount++;
            if (trust >= 70f) highTrustCount++;
            if (trust >= 40f && trust < 70f) midTrustCount++;
            if (trust < 40f) lowTrustCount++;
        }

        // 🟢 UTOPIAN WIN
        if (style == LeadershipStyle.Utopian && highTrustCount == 4)
        {
            TriggerEnding("Utopian Victory", "You united the flock in peace and harmony.", utopianFace);
            return;
        }

        // 🟢 DICTATOR WIN
        if (style == LeadershipStyle.Dictator && lowTrustCount >= 3)
        {
            FactionData loyalFaction = null;
            foreach (var faction in factions)
            {
                if (faction.trustLevel >= faction.friendlyThreshold)
                {
                    loyalFaction = faction;
                    break;
                }
            }

            if (loyalFaction != null)
            {
                TriggerEnding("Dictator Victory", $"You crushed resistance with the {loyalFaction.factionName}'s support.", dictatorFace);
                return;
            }
        }

        // 🟢 PRAGMATIST WIN
        if (style == LeadershipStyle.Pragmatist && midTrustCount >= 2)
        {
            TriggerEnding("Pragmatist Victory", "You balanced loyalty and survival in a divided world.", pragmatistFace);
            return;
        }

        // 🔴 ALIENATED LOSE
        if (lowTrustCount == 4)
        {
            TriggerEnding("Isolation Ending", "No one trusts you. You are alone.", isolationFace);
            return;
        }

        // 🔴 REVOLT LOSE
        if (hostileCount >= 2)
        {
            TriggerEnding("Revolt Ending", "The factions unite — against you.", revoltFace);
            return;
        }

        Debug.Log("[GameEndingManager] No ending triggered. Conditions not met.");
    }

    public bool AllChainsResolved()
    {
        return
            MissionTracker.Instance.IsChainComplete(sparrows.missionChain) &&
            MissionTracker.Instance.IsChainComplete(crows.missionChain) &&
            MissionTracker.Instance.IsChainComplete(blueJays.missionChain) &&
            MissionTracker.Instance.IsChainComplete(doves.missionChain);
    }

    private void TriggerEnding(string title, string message, Sprite face)
    {
        Debug.Log($"🏁 {title}: {message}");

        Time.timeScale = 0f;

        ShowEndingDialogue(title, message, face);
    }

    private void ShowEndingDialogue(string title, string message, Sprite pigeonFace)
    {
        var choices = new List<DialogueChoice>
        {
            new DialogueChoice("Restart", () =>
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            })
        };

        DialogueSystem.Instance.ShowDialogue($"{title}\n\n{message}", pigeonFace, choices);
    }
}
