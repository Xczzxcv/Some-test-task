using Core.Models;
using UnityEngine;

namespace Ui
{
internal class MainScreenController : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private ActionButtonsSpawner actionButtonsSpawner;

    public void Setup(IInventory inventory)
    {
        inventoryController.Setup(inventory);
        actionButtonsSpawner.Spawn(inventory);
    }
}
}