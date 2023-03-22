using Aboba.Network.Common;

namespace Aboba.Network.Client
{
  public interface IClientRequest_ClientSide<out TDto> where TDto : IDto
  {
    int Key { get; }
    
    TDto Payload { get; }
  }
}