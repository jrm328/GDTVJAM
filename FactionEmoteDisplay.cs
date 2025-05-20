using UnityEngine;

public class FactionEmoteDisplay : MonoBehaviour
{
    public FactionData faction;
    public SpriteRenderer emoteRenderer;
    public Sprite hostileSprite;
    public Sprite neutralSprite;
    public Sprite friendlySprite;

    private TrustState lastState;

    private void Start()
    {
        UpdateEmote(); // Initial state
    }

    private void Update()
    {
        if (faction == null || emoteRenderer == null) return;

        TrustState currentState = faction.GetTrustState();
        if (currentState != lastState)
        {
            UpdateEmote();
            lastState = currentState;
        }
    }

    private void UpdateEmote()
    {
        switch (faction.GetTrustState())
        {
            case TrustState.Hostile:
                emoteRenderer.sprite = hostileSprite;
                break;
            case TrustState.Neutral:
                emoteRenderer.sprite = neutralSprite;
                break;
            case TrustState.Friendly:
                emoteRenderer.sprite = friendlySprite;
                break;
        }
    }

   
}
