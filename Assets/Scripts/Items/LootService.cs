using Aboba.Items.Descriptors;

namespace Aboba.Items
{
  public class LootService
  {
    private readonly InventoryService _inventoryService;
    
    public void CollectLoot(ulong ownerId, ClientLootObject item)
    {
      var result = item.ItemDescriptor switch
                        {
                          InventoryItemDescriptor inventoryItemDescriptor => _inventoryService.AddItem(ownerId, inventoryItemDescriptor),
                        };

      if(result)
        item.NetworkObject.Despawn();
    }

    public LootService(InventoryService inventoryService) => _inventoryService = inventoryService;
  }
}