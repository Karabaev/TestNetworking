using System.Linq;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Common.Model;
using Aboba.Network.Client;
using Cysharp.Threading.Tasks;

namespace Aboba.Items.Client.Services
{
  public class ClientInventoryService
  {
    private readonly IClientRequestManager _requestManager;
    private readonly ItemsReference _itemsReference;

    public bool Initialized { get; private set; }
    
    public Inventory Inventory { get; private set; } = null!;
    
    public async UniTask InitializeAsync()
    {
      var inventoryDto = await _requestManager.RequestUserInventoryAsync();
      Inventory = new Inventory(inventoryDto.Slots.Count);
      
      foreach(var slot in inventoryDto.Slots)
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

    public ClientInventoryService(IClientRequestManager requestManager, ItemsReference itemsReference)
    {
      _requestManager = requestManager;
      _itemsReference = itemsReference;
    }
  }
}