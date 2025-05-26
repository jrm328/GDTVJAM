using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public LeadershipStyle PlayerLeadershipStyle { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetLeadershipStyle(LeadershipStyle style)
    {
        PlayerLeadershipStyle = style;
    }
}
