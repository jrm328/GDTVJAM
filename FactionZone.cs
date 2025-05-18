using UnityEngine;

public class FactionZone : MonoBehaviour
{
    public string factionName;

    private void OnMouseDown()
    {
        Debug.Log("Clicked on faction: " + factionName);
        GameManager.Instance.OpenFactionPanel(factionName);
    }
}
