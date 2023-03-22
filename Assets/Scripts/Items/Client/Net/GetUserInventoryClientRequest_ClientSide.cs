using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client;

namespace Aboba.Items.Client.Net
{
  public class GetUserInventoryClientRequest_ClientSide : IClientRequest_ClientSide<GetUserInventoryDto>
  {
    public const int RequestKey = 0;
    
    public int Key => RequestKey;

    public GetUserInventoryDto Payload { get; }

    public GetUserInventoryClientRequest_ClientSide(ulong userId) => Payload = new GetUserInventoryDto(userId);
  }
}