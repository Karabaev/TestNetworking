using System.Collections.Generic;
using Aboba.Infrastructure;
using Aboba.Network;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Aboba.Items.UI
{
  public class InventoryPanel : MonoBehaviour
  {
    [Inject]
    private CurrentPlayerService _currentPlayerService = null!;
    [Inject]
    private InventoryService _inventoryService = null!;
    [Inject]
    private FromResourceFactory _fromResourceFactory = null!;

    private Inventory _currentInventory = null!;

    private IReadOnlyList<InventorySlotPanel> _slotPanels = null!;

    public async UniTask InitializeAsync()
    {
      _currentInventory = _inventoryService.GetInventory(_currentPlayerService.CurrentPlayerId);

      _slotPanels = await _fromResourceFactory.CreateAsync<InventorySlotPanel>("UI/pfInventorySlotPanel", _currentInventory.Slots.Count);

      foreach(var panel in _slotPanels)
        transform.AddChild(panel);

      _currentInventory.ItemAdded += OnItemAdded;
      _currentInventory.ItemRemoved += OnItemRemoved;
    }

    private void OnDestroy()
    {
      _currentInventory.ItemAdded -= OnItemAdded;
      _currentInventory.ItemRemoved -= OnItemRemoved;
    }

    private void OnItemAdded(int slotIndex) => _slotPanels[slotIndex].Item = _currentInventory.Slots[slotIndex];

    private void OnItemRemoved(int slotIndex) => _slotPanels[slotIndex].Item = _currentInventory.Slots[slotIndex];
  }
}