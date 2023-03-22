using Aboba.Items.Common.Net.Dto;
using Aboba.Items.Server.Services;
using Aboba.Network.Common;
using VContainer;

namespace Aboba.Items.Common.Net
{
  public class GetUserInventoryClientRequest : ClientRequest<GetUserInventoryDto>
  {
    public const int RequestKey = 0;

#region Client
    
    public override int Key => RequestKey;
    
    public GetUserInventoryClientRequest(ulong userId) : base( new GetUserInventoryDto(userId)) { }

#endregion

#region Server

    public override IDto Execute(IObjectResolver objectResolver, IDto payload)
    {
      var dto = (GetUserInventoryDto)payload;
      var inventory = objectResolver.Resolve<ServerInventoryService>().GetInventory(dto.UserId);
      return new GetUserInventoryResponse(inventory);
    }
    
    public GetUserInventoryClientRequest() { }

#endregion
  }
}