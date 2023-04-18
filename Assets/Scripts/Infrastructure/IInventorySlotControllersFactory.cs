using Core.Models;
using Ui;
using UnityEngine;

namespace Infrastructure
{
internal interface IInventorySlotControllersFactory
{
    InventorySlotController Create(IInventorySlot slot, Transform parent);
}
}