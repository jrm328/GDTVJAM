using UnityEngine;

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
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on faction: " + factionName);
        //GameManager.Instance.OpenFactionPanel(factionName);

        if (factionInteraction != null)
        {
            factionInteraction.Interact();
        }
        else
        {
            Debug.LogWarning("FactionInteraction not found on click.");
        }
    }
}
