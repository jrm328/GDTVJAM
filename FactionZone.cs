using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FactionZone : MonoBehaviour
{
    public string factionName;
    public FactionInteraction factionInteraction;

    private void Awake()
    {
        if (factionInteraction == null)
        {
            factionInteraction = GetComponent<FactionInteraction>();
        }

        Collider col = GetComponent<Collider>();
        col.isTrigger = true; // Ensure it's a trigger
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"[FactionZone] Player entered {factionName}'s area.");
        if (factionInteraction != null)
        {
            factionInteraction.Interact();
        }
        else
        {
            Debug.LogWarning($"[FactionZone] No FactionInteraction found on {factionName}.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"[FactionZone] Player exited {factionName}'s area.");
        DialogueSystem.Instance.Close();
    }
}
