using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Core.Data
{
[Serializable]
internal class InventorySlotData : IGameDataPiece
{
    [JsonProperty("item")]
    [CanBeNull] public InventoryItemData Item;
    [JsonProperty("is_active")]
    public bool IsActive = true;
}
}