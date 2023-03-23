using Aboba.Network.Common;

namespace Aboba.Network.Server.Services
{
  public interface IServerCommandSender
  {
    void SendCommand(ulong clientId, IServerCommand command);
  }
}