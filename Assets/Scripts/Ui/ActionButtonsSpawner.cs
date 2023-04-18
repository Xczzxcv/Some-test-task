using System.Collections.Generic;
using Core.Models;
using UnityEngine;
using Zenject;

namespace Ui
{
internal class ActionButtonsSpawner : MonoBehaviour
{
    [SerializeField] private List<ActionButtonController> actionButtonPrefabs;
    [SerializeField] private Transform actionButtonsRoot;
    
    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Spawn(IInventory inventory)
    {
        foreach (var actionButtonPrefab in actionButtonPrefabs)
        {
            var actionButtonController =
                _container.InstantiatePrefabForComponent<ActionButtonController>(
                    actionButtonPrefab, actionButtonsRoot);
            actionButtonController.Init(inventory);
        }
    }
}
}
