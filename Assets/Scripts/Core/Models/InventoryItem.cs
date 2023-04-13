using Core.Configs;
using Core.Data;

namespace Core.Models
{
internal abstract class InventoryItem<TConfig, TData> : IInventoryItem
    where TConfig : InventoryItemConfig
    where TData : InventoryItemData
{
    public string Id => Config.Id;
    public int Amount => Data.Amount;

    protected readonly TConfig Config;
    protected readonly TData Data;

    protected InventoryItem(TConfig config, TData data)
    {
        Config = config;
        Data = data;
    }

    public override string ToString() => $"Inv Item '{Id}'";
}
}