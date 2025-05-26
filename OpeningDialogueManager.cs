using System.Collections.Generic;
using UnityEngine;

public class OpeningDialogueManager : MonoBehaviour
{
    public Sprite pigeonIcon;

    public ItemData breadcrumbItem;
    public ItemData shinyItem;
    public ItemData seedsItem;

    public FactionData sparrowFaction;
    public FactionData crowFaction;
    public FactionData blueJayFaction;

    private void Start()
    {
        ShowOpeningDialogue();
    }

    public void ShowOpeningDialogue()
    {
        var choices = new List<DialogueChoice>
        {
            new DialogueChoice("Dictator: Secure the breadcrumbs!", () => HandleOpeningPath(LeadershipStyle.Dictator)),
            new DialogueChoice("Utopian: Offer peace to the crows.", () => HandleOpeningPath(LeadershipStyle.Utopian)),
            new DialogueChoice("Pragmatist: Trade with the blue jays.", () => HandleOpeningPath(LeadershipStyle.Pragmatist))
        };

        DialogueSystem.Instance.ShowDialogue(
            "A crisis looms over the flock. What kind of leader will you be?",
            pigeonIcon,
            choices
        );
    }

    private void HandleOpeningPath(LeadershipStyle style)
    {
        GameStateManager.Instance.SetLeadershipStyle(style);

        // Apply leadership effects (trust, item spawns, etc.)
        GameInitializer initializer = Object.FindFirstObjectByType<GameInitializer>();
        if (initializer != null)
        {
            initializer.ApplyLeadershipStyle(style);
        }
        else
        {
            Debug.LogError("❌ GameInitializer not found in scene.");
        }
    }
}
