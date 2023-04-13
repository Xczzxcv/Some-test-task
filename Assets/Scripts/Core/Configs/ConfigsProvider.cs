using System.Collections.Generic;
using UnityEngine;

namespace Core.Configs
{
internal partial class ConfigsProvider : MonoBehaviour, IConfigsProvider
{
    [SerializeField] private List<InventoryItemConfig> itemConfigs;
    [SerializeField] private List<InventoryItemViewConfig> itemViewConfigs;
    [SerializeField] private InventoryConfig inventoryConfig;

    public IReadOnlyConfigCollection<InventoryItemConfig> InventoryItems => _items;
    public IReadOnlyConfigCollection<InventoryItemViewConfig> InventoryItemViews => _itemViews;
    public InventoryConfig Inventory => inventoryConfig;

    private readonly GameConfigCollection<InventoryItemConfig> _items = new();
    private readonly GameConfigCollection<InventoryItemViewConfig> _itemViews = new();

    public void Init()
    {
        Validate();

        InitItems();
        InitItemViews();
    }

    private void InitItems()
    {
        foreach (var itemConfig in itemConfigs)
        {
            _items.Add(itemConfig.Id, itemConfig);
        }
    }

    private void InitItemViews()
    {
        foreach (var itemViewConfig in itemViewConfigs)
        {
            _itemViews.Add(itemViewConfig.Id, itemViewConfig);
        }
    }
}
}