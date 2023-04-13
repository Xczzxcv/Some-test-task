using System;
using Newtonsoft.Json;

namespace Core.Data
{
[Serializable]
internal class GameData : IGameDataPiece
{
    [JsonProperty("inventory")]
    public InventoryData Inventory;
}
}