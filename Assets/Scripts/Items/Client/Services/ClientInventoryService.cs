using System.Linq;
using Aboba.Items.Client.Net;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Common.Model;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client.Service;
using Aboba.Player;
using Cysharp.Threading.Tasks;

namespace Aboba.Items.Client.Services
{
  public class ClientInventoryService
  {
    private readonly IClientRequestManager _requestManager;
    private readonly ItemsReference _itemsReference;
    private readonly CurrentPlayerService _currentPlayerService;

    public bool Initialized { get; private set; }
    
    public Inventory Inventory { get; private set; } = null!;
    
    public async UniTask InitializeAsync()
    {
      var userId = _currentPlayerService.CurrentPlayerId;
      var response = await _requestManager.SendRequestAsync<GetUserInventoryDto, GetUserInventoryResponse>(new GetUserInventoryClientRequest_ClientSide(userId));
      Inventory = new Inventory(response.Slots.Count);
      
      foreach(var slot in response.Slots)
      {
        if(string.IsNullOrEmpty(slot.ItemId))
          continue;

        var descriptor = _itemsReference.Items.First(i => i.Id == slot.ItemId);
        Inventory.AddItemsToSlot(slot.InventoryIndex, (InventoryItemDescriptor)descriptor, slot.Count);
      }

      Initialized = true;
    }

    public bool AddItems(string itemId, int count)
    {
      var descriptor = _itemsReference.Items.First(i => i.Id == itemId);
      return Inventory.AddItems((InventoryItemDescriptor)descriptor, count);
    }

    public ClientInventoryService(IClientRequestManager requestManager, ItemsReference itemsReference, CurrentPlayerService currentPlayerService)
    {
      _requestManager = requestManager;
      _itemsReference = itemsReference;
      _currentPlayerService = currentPlayerService;
    }
  }
}