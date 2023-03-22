using Aboba.Items.Common.Net.Dto;
using Aboba.Items.Server.Services;
using Aboba.Network.Common;
using Aboba.Network.Server;
using VContainer;

namespace Aboba.Items.Server.Net
{
  public class GetUserInventoryClientRequest_ServerSide : IClientRequest_ServerSide
  {
    public IDto Execute(IObjectResolver objectResolver, IDto payload)
    {
      var dto = (GetUserInventoryDto)payload;
      var inventory = objectResolver.Resolve<ServerInventoryService>().GetInventory(dto.UserId);
      return new GetUserInventoryResponse(inventory);
    }
  }
}