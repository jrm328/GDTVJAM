using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Peckonomics/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
}
