using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_Text dialogueText;
    public Image speakerIcon;
    public Transform choiceContainer;
    public Button choiceButtonPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
    }

    public void ShowDialogue(string text, Sprite icon, List<DialogueChoice> choices)
    {
        panel.SetActive(true);
        dialogueText.text = text;

        if (speakerIcon != null)
        {
            speakerIcon.sprite = icon;
            speakerIcon.gameObject.SetActive(icon != null);
        }

        // Clear old choices
        foreach (Transform child in choiceContainer)
            Destroy(child.gameObject);

        // Create buttons for each choice
        foreach (var choice in choices)
        {
            var btn = Instantiate(choiceButtonPrefab, choiceContainer);
            btn.GetComponentInChildren<TMP_Text>().text = choice.text;
            btn.onClick.AddListener(() =>
            {
                choice.onSelect?.Invoke();
                Close();
            });
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}

[Serializable]
public class DialogueChoice
{
    public string text;
    public Action onSelect;

    public DialogueChoice(string text, Action onSelect)
    {
        this.text = text;
        this.onSelect = onSelect;
    }
}
