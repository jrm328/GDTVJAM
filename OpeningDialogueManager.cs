using System.Collections.Generic;
using UnityEngine;

public class OpeningDialogueManager : MonoBehaviour
{
    public Sprite pigeonIcon;
    public ItemData breadcrumbItem, shinyItem, wormItem, bottleCapItem;
    public Transform playerTransform;
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
            new DialogueChoice("Dictator: Secure the breadcrumbs!", HandleDictatorPath),
            new DialogueChoice("Utopian: Offer peace to the crows.", HandleUtopianPath),
            new DialogueChoice("Pragmatist: Trade with the blue jays.", HandlePragmatistPath)
        };

        DialogueSystem.Instance.ShowDialogue(
            "A crisis looms over the flock. What kind of leader will you be?",
            pigeonIcon,
            choices
        );
    }

    private void HandleDictatorPath()
    {
        MissionManager.Instance.StartBreadcrumbMission(breadcrumbItem, playerTransform.position, sparrowFaction);
    }

    private void HandleUtopianPath()
    {
        MissionManager.Instance.StartShinyMission(shinyItem, playerTransform.position, crowFaction);
    }

    private void HandlePragmatistPath()
    {
        MissionManager.Instance.StartWormMission(wormItem, bottleCapItem, playerTransform.position, blueJayFaction);
    }
}
