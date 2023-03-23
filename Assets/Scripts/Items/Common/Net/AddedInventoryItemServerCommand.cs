using Aboba.Items.Client.Services;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Common;
using VContainer;

namespace Aboba.Items.Common.Net
{
  public class AddedInventoryItemServerCommand : IServerCommand
  {
    public const int CommandKey = 0;

#region Server

    public int Key => CommandKey;

    public IDto Payload { get; }

    public AddedInventoryItemServerCommand(string itemId, int count) => Payload = new AddedInventoryItemDto(itemId, count);
    
#endregion

#region Client

    public void Execute(IDto payload, IObjectResolver objectResolver)
    {
      var dto = (AddedInventoryItemDto)payload;
      objectResolver.Resolve<ClientInventoryService>().AddItems(dto.ItemId, dto.Count);
    }

    public AddedInventoryItemServerCommand() => Payload = null!;

#endregion
  }
}