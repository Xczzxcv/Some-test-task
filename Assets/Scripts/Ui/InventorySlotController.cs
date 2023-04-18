using System;
using Core.Configs;
using Core.Models;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
internal class InventorySlotController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private TextMeshProUGUI itemCounter;
    [SerializeField] private Button slotBtn;
    [Header("Background colors")]
    [SerializeField] private Color commonColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color inactiveColor;

    private IConfigsProvider _configsProvider;
    public IInventorySlot Slot { get; private set; }

    public event Action<InventorySlotController> SlotSelected;
    
    private bool _selected;

    [Inject]
    private void Construct(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;
    }

    private void Awake()
    {
        slotBtn.onClick.AddListener(OnSlotClicked);
    }

    public void Setup(IInventorySlot inventorySlot)
    {
        Slot = inventorySlot;
        Subscribe();
        UpdateView();
    }

    public void SetSelected(bool selected)
    {
        if (!Slot.IsActive)
        {
            return;
        }
        
        _selected = selected;
        UpdateBackground();
    }

    private void UpdateView()
    {
        UpdateItemIcon();
        UpdateItemCounter();
        UpdateBackground();
    }

    private void UpdateBackground()
    {
        if (!Slot.IsActive)
        {
            backgroundImg.color = inactiveColor;
            return;
        }

        backgroundImg.color = _selected 
            ? selectedColor 
            : commonColor;
    }

    private void Subscribe()
    {
        Slot.SlotUpdated += OnSlotUpdated;
    }

    private void Unsubscribe()
    {
        Slot.SlotUpdated -= OnSlotUpdated;
    }

    private void OnSlotClicked()
    {
        SlotSelected?.Invoke(this);
    }

    private void OnSlotUpdated()
    {
        UpdateView();
    }

    private void UpdateItemIcon()
    {
        if (Slot.Item == null)
        {
            HideIcon();
            return;
        }

        if (!_configsProvider.InventoryItemViews.TryGetValue(Slot.Item.Id,
                out var itemViewConfig))
        {
            HideIcon();
            return;
        }

        ShowIcon(itemViewConfig);
    }

    private void HideIcon()
    {
        itemIcon.enabled = false;
    }

    private void ShowIcon(InventoryItemViewConfig itemViewConfig)
    {
        itemIcon.enabled = true;
        itemIcon.sprite = itemViewConfig.Sprite;
    }

    private void UpdateItemCounter()
    {
        itemCounter.text = GetItemCounterText(Slot.Item);
    }

    private static string GetItemCounterText([CanBeNull] IInventoryItem item)
    {
        if (item == null)
        {
            return string.Empty;
        }

        return item.Amount > 1
            ? item.Amount.ToString()
            : string.Empty;
    }
}
}