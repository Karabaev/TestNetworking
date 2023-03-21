using Aboba.Items.Common.Net.Dto;
using Aboba.Items.Server.Services;

namespace Aboba.Network.Server
{
  public class ClientRequestReceiver
  {
    private readonly ServerInventoryService _serverInventoryService;

    public InventoryDto GetUserInventory(ulong clientId)
    {
      var inventory = _serverInventoryService.GetInventory(clientId);
      return new InventoryDto(inventory);
    }
    
    public ClientRequestReceiver(ServerInventoryService serverInventoryService) => _serverInventoryService = serverInventoryService;
  }
}