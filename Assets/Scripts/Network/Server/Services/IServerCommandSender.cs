using Aboba.Network.Common;

namespace Aboba.Network.Server.Services
{
  public interface IServerCommandSender
  {
    void SendCommand<TDto>(ulong clientId, IServerCommand_ServerSide<TDto> command) where TDto : IDto;
  }
}