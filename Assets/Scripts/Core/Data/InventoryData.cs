using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Data
{
[Serializable]
internal class InventoryData : IGameDataPiece
{
    [JsonProperty("slots")]
    public List<InventorySlotData> Slots;
}
}