using System.Linq;
using Core.Configs;
using Core.Models;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Ui
{
internal class AddAllTypesItemsActionButtonController : ActionButtonController
{
    [SerializeField] private int addedItemAmount;

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
        var inventoryItemConfigs = _configsProvider.InventoryItems.Values.ToArray();

        AddHeadArmor(inventoryItemConfigs);
        AddTorsoArmor(inventoryItemConfigs);
        AddWeapon(inventoryItemConfigs);
    }

    private void AddHeadArmor(InventoryItemConfig[] inventoryItemConfigs)
    {
        var headArmorConfigs = inventoryItemConfigs.Where(config =>
            config is ArmorInventoryItemConfig {BodyPart: BodyPart.Head}).ToArray();
        AddRandomFrom(headArmorConfigs);
    }

    private void AddTorsoArmor(InventoryItemConfig[] inventoryItemConfigs)
    {
        var torsoArmorConfigs = inventoryItemConfigs.Where(config =>
            config is ArmorInventoryItemConfig {BodyPart: BodyPart.Torso}).ToArray();
        AddRandomFrom(torsoArmorConfigs);
    }

    private void AddWeapon(InventoryItemConfig[] inventoryItemConfigs)
    {
        var weaponConfigs = inventoryItemConfigs.Where(config =>
            config is WeaponInventoryItemConfig).ToArray();
        AddRandomFrom(weaponConfigs);
    }

    private void AddRandomFrom(InventoryItemConfig[] itemConfigs)
    {
        var randomItemConfig = itemConfigs[Random.Range(0, itemConfigs.Length)];
        var itemId = randomItemConfig.Id;
        var item = _inventoryItemsFactory.Create(itemId, addedItemAmount);
        var putIntoResult = Inventory.TryPutItem(item);
        InventoryHelper.LogPutItemResults(putIntoResult, addedItemAmount, itemId);
    }
}
}