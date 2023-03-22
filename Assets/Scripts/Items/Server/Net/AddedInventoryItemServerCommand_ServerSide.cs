using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Server;

namespace Aboba.Items.Server.Net
{
  public class AddedInventoryItemServerCommand_ServerSide : IServerCommand_ServerSide<AddedInventoryItemDto>
  {
    public const int CommandKey = 0;
    
    public int Key => CommandKey;

    public AddedInventoryItemDto Payload { get; }

    public AddedInventoryItemServerCommand_ServerSide(string itemId, int count) => Payload = new AddedInventoryItemDto(itemId, count);
  }
}