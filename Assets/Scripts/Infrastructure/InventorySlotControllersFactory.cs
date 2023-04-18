using Core.Models;
using Ui;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
internal class InventorySlotControllersFactory : MonoBehaviour, IInventorySlotControllersFactory
{
    [SerializeField] private InventorySlotController slotPrefab;
    
    private DiContainer _container;

    public void Init(DiContainer container)
    {
        _container = container;
    }

    public InventorySlotController Create(IInventorySlot slot, Transform parent)
    {
        var inventorySlotController = _container
            .InstantiatePrefabForComponent<InventorySlotController>(slotPrefab, parent);
        inventorySlotController.Setup(slot);
        return inventorySlotController;
    }
}
}