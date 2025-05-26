using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactionTrustUI : MonoBehaviour
{
    [Header("References")]
    public FactionData factionData;
    public Image factionIcon;
    public TMP_Text trustText;
    public TMP_Text trustStateText;

    public void UpdateTrust()
    {
        if (factionData == null) return;

        factionIcon.sprite = factionData.factionIcon;
        trustText.text = $"Trust: {factionData.trustLevel:F0}";
        trustStateText.text = factionData.GetTrustState().ToString();
    }
}
