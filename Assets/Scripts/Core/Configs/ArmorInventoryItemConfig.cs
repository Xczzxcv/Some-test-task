using Core.Models;
using UnityEngine;

namespace Core.Configs
{
[CreateAssetMenu(menuName = "Configs/Inventory Item/Armor", fileName = "ArmorInventoryItemConfig", order = 0)]
public class ArmorInventoryItemConfig : InventoryItemConfig
{
    public BodyPart BodyPart;
    public float ArmorAmount;
}
}