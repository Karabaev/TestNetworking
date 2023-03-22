using Aboba.Items.Client.Services;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client;
using Aboba.Network.Common;
using VContainer;

namespace Aboba.Items.Client.Net
{
  public class AddedInventoryItemServerCommand_ClientSide : IServerCommand_ClientSide
  {
    public void Execute(IDto payload, IObjectResolver objectResolver)
    {
      var dto = (AddedInventoryItemDto)payload;
      objectResolver.Resolve<ClientInventoryService>().AddItems(dto.ItemId, dto.Count);
    }
  }
}