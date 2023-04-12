using UnityEngine;

namespace Core.Configs
{
[CreateAssetMenu(menuName = "Configs/Inventory Item View", fileName = "InventoryItemViewConfig", order = 0)]
public class InventoryItemViewConfig : ScriptableObject, IGameConfig
{
    public string Id;
    public Sprite Sprite;
}
}