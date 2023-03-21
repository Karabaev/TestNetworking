using System.Collections.Generic;
using Aboba.Infrastructure;
using Aboba.Items.Client.Services;
using Aboba.Items.Common.Model;
using Aboba.Network;
using Aboba.Network.Client;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Aboba.Items.Client.UI
{
  public class InventoryPanel : MonoBehaviour
  {
    
    [Inject]
    private CurrentPlayerService _currentPlayerService = null!;
    [Inject]
    private FromResourceFactory _fromResourceFactory = null!;
    [Inject]
    private ClientInventoryService _clientInventoryService = null!;

    private IReadOnlyList<InventorySlotPanel> _slotPanels = null!;

    private Inventory Inventory => _clientInventoryService.Inventory;
    
    public async UniTask InitializeAsync()
    {
      _slotPanels = await _fromResourceFactory.CreateAsync<InventorySlotPanel>("UI/pfInventorySlotPanel", Inventory.Slots.Count);

      foreach(var panel in _slotPanels)
        transform.AddChild(panel);

      Inventory.ItemAdded += OnItemAdded;
      Inventory.ItemRemoved += OnItemRemoved;
    }

    private void OnDestroy()
    {
      Inventory.ItemAdded -= OnItemAdded;
      Inventory.ItemRemoved -= OnItemRemoved;
    }

    private void OnItemAdded(int slotIndex) => _slotPanels[slotIndex].Item = Inventory.Slots[slotIndex];

    private void OnItemRemoved(int slotIndex) => _slotPanels[slotIndex].Item = Inventory.Slots[slotIndex];
  }
}