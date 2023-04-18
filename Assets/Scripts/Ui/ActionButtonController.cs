using Core.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
internal abstract class ActionButtonController : MonoBehaviour
{
    [SerializeField] private Button actionBtn;
    
    protected IInventory Inventory;

    public void Init(IInventory inventory)
    {
        Inventory = inventory;
    }

    private void Awake()
    {
        actionBtn.onClick.AddListener(OnActionBtnClick);
    }

    private void OnActionBtnClick()
    {
        PerformAction();
    }

    protected abstract void PerformAction();
}
}