using Aboba.Items.Client;
using Aboba.Items.Common.Descriptors;

namespace Aboba.Items.Server.Services
{
  public class ServerLootService
  {
    private readonly ServerInventoryService _serverInventoryService;
    
    public void CollectLoot(ulong ownerId, ClientLootObject item)
    {
      var result = item.ItemDescriptor switch
                        {
                          InventoryItemDescriptor inventoryItemDescriptor => _serverInventoryService.AddItem(ownerId, inventoryItemDescriptor),
                        };

      if(result)
        item.NetworkObject.Despawn();
    }

    public ServerLootService(ServerInventoryService serverInventoryService) => _serverInventoryService = serverInventoryService;
  }
}