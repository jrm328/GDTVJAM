using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactionPanelUI : MonoBehaviour
{
    public TMP_Text factionNameText;
    public Slider trustSlider;
    private FactionData currentFaction;

    public void Open(FactionData faction)
    {
        currentFaction = faction;
        factionNameText.text = faction.factionName;
        trustSlider.value = faction.trustLevel / 100f;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void IncreaseTrust(float amount)
    {
        currentFaction.trustLevel += amount;
        trustSlider.value = currentFaction.trustLevel / 100f;
    }
}
