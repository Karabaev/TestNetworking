using Aboba.Network.Common;

namespace Aboba.Network.Server
{
  public interface IServerCommand_ServerSide<out TDto> where TDto : IDto
  {
    int Key { get; }

    TDto Payload { get; }
  }
}