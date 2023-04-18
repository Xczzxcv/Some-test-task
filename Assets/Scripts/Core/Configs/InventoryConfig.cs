using UnityEngine;

namespace Core.Configs
{
[CreateAssetMenu(menuName = "Configs/Inventory Config", fileName = "InventoryConfig", order = 0)]
internal class InventoryConfig : ScriptableObject, IGameConfig
{
    public int Size;
    public int InitiallyUnlockedSlotsAmount;
    public float UnlockPrice;
}
}