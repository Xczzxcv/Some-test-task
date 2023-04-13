using System;
using Newtonsoft.Json;

namespace Core.Data
{
[Serializable]
internal abstract class InventoryItemData : ITypedGameData, IGameDataPiece
{
    public string Type { get; set; }

    [JsonProperty("item_id")]
    public string ItemId;
    [JsonProperty("amount")]
    public int Amount;
}
}