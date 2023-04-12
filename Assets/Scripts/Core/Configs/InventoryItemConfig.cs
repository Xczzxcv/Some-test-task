using UnityEngine;

namespace Core.Configs
{
public abstract class InventoryItemConfig : ScriptableObject, IGameConfig
{
    public string Id;
    public float Weight;
    public int MaxStackAmount;
}
}