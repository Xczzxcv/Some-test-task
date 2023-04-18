using System.Linq;
using Core.Configs;
using Core.Models;
using Infrastructure;
using Zenject;

namespace Ui
{
internal class AddProjectilesActionButtonController : ActionButtonController
{
    private IConfigsProvider _configsProvider;
    private IInventoryItemsFactory _inventoryItemsFactory;

    [Inject]
    private void Construct(IConfigsProvider configsProvider,
        IInventoryItemsFactory inventoryItemsFactory)
    {
        _configsProvider = configsProvider;
        _inventoryItemsFactory = inventoryItemsFactory;
    }
    
    protected override void PerformAction()
    {
        var projectileConfigs = _configsProvider.InventoryItems.Values.Where(config =>
            config is ConsumableInventoryItemConfig {ConsumableType: ConsumableItemType.Projectile});

        foreach (var projectileConfig in projectileConfigs)
        {
            var itemId = projectileConfig.Id;
            var targetAmount = projectileConfig.MaxStackAmount;
            var projectileItem = _inventoryItemsFactory.Create(itemId, targetAmount);
            var putItemAmount = Inventory.TryPutItem(projectileItem);
            InventoryHelper.LogPutItemResults(putItemAmount, targetAmount, itemId);
        }
    }
}
}