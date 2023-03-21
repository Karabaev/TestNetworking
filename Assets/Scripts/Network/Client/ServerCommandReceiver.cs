using Aboba.Items.Client.Services;
using Aboba.Items.Common.Net.Dto;

namespace Aboba.Network.Client
{
  public class ServerCommandReceiver
  {
    private readonly ClientInventoryService _clientInventoryService;

    public void OnItemsAdded(AddedInventoryItemDto dto)
    {
      _clientInventoryService.AddItems(dto.ItemId, dto.Count);
    }

    public ServerCommandReceiver(ClientInventoryService clientInventoryService) => _clientInventoryService = clientInventoryService;
  }
}