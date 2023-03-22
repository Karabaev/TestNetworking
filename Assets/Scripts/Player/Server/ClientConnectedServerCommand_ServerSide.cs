using Aboba.Network.Server;
using Aboba.Player.Common.Net;

namespace Aboba.Player.Server
{
  public class ClientConnectedServerCommand_ServerSide : IServerCommand_ServerSide<ClientConnectedDto>
  {
    public const int CommandKey = 2;
    
    public int Key => CommandKey;

    public ClientConnectedDto Payload { get; }

    public ClientConnectedServerCommand_ServerSide(ulong clientId) => Payload = new ClientConnectedDto(clientId);
  }
}