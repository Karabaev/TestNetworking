using Aboba.Items.Client.Services;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client;
using Aboba.Network.Common;
using VContainer;

namespace Aboba.Items.Client.Net
{
  public class AddedInventoryItemServerCommand_ClientSide : IServerCommand_ClientSide
  {
    private readonly IObjectResolver _objectResolver;
    
    public void Execute(IDto payload)
    {
      var dto = (AddedInventoryItemDto)payload;
      _objectResolver.Resolve<ClientInventoryService>().AddItems(dto.ItemId, dto.Count);
    }

    public AddedInventoryItemServerCommand_ClientSide(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
    }
  }
}