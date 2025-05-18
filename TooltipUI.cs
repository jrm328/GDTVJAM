using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public GameObject panel;
    public TMP_Text tooltipText;
    public float displayDuration = 2.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowTooltip(string message)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ShowTooltipCoroutine(message));
    }

    private System.Collections.IEnumerator ShowTooltipCoroutine(string message)
    {
        tooltipText.text = message;
        panel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        panel.SetActive(false);
    }
}