using System;
using System.Collections;
using System.Collections.Generic;
using Core.Models;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
internal class InventoryView : MonoBehaviour
{
    [SerializeField] private Transform slotsRoot;
    [SerializeField] private Button unlockSlotsBtn;
    [SerializeField] private TextMeshProUGUI unlockBtnText;
    [SerializeField] private float unlockBtnShowDuration;

    public event Action<InventorySlotController> SlotSelected;
    public event Action UnlockSlotsBtnClick;
    
    private IInventorySlotControllersFactory _inventorySlotControllersFactory;
    private readonly List<InventorySlotController> _slots = new();
    private InventorySlotController _moveSelectionSrc;
    private Coroutine _unlockSlotsBtnShowCoroutine;

    [Inject]
    private void Construct(IInventorySlotControllersFactory inventorySlotControllersFactory)
    {
        _inventorySlotControllersFactory = inventorySlotControllersFactory;
    }

    private void Awake()
    {
        unlockSlotsBtn.onClick.AddListener(OnUnlockSlotsBtnClick);
    }

    public void Setup(IInventory inventory, bool showUnlockSlotsBtn)
    {
        SetupSlots(inventory);
        SetupUnlockBtn(inventory, showUnlockSlotsBtn);
    }

    private void SetupSlots(IInventory inventory)
    {
        int i;
        for (i = 0; i < inventory.Slots.Count; i++)
        {
            var slot = inventory.Slots[i];

            InventorySlotController slotController;
            if (i < _slots.Count)
            {
                slotController = _slots[i];
            }
            else
            {
                slotController = _inventorySlotControllersFactory.Create(slot, slotsRoot);
                slotController.SlotSelected += OnSlotSelected;
                _slots.Add(slotController);
            }

            slotController.Setup(slot);
        }

        for (; i < _slots.Count; i++)
        {
            Destroy(_slots[i].gameObject);
        }
    }

    private void SetupUnlockBtn(IInventory inventory, bool showUnlockSlotsBtn)
    {
        unlockBtnText.text = $"Unlock inventory slots for {inventory.UnlockSlotsPrice}";
    }

    private void OnSlotSelected(InventorySlotController slotController)
    {
        SlotSelected?.Invoke(slotController);
    }

    public void ShowUnlockBtn()
    {
        var isAlreadyShowing = _unlockSlotsBtnShowCoroutine != null;
        if (isAlreadyShowing)
        {
            return;
        }

        unlockSlotsBtn.gameObject.SetActive(true);
        _unlockSlotsBtnShowCoroutine = StartCoroutine(DeferredHideUnlockBtnCoroutine());

        IEnumerator DeferredHideUnlockBtnCoroutine()
        {
            yield return new WaitForSeconds(unlockBtnShowDuration);
            HideUnlockSlotsBtn();
            _unlockSlotsBtnShowCoroutine = null;
        }
    }

    private void HideUnlockSlotsBtn()
    {
        unlockSlotsBtn.gameObject.SetActive(false);
    }

    private void OnUnlockSlotsBtnClick()
    {
        HideUnlockSlotsBtn();
        UnlockSlotsBtnClick?.Invoke();
    }
}
}