using Aboba.Network.Client.Service;
using Aboba.Network.Common;
using Unity.Netcode;
using VContainer;

namespace Aboba.Network.Server.Services
{
  /// <summary>
  /// Отправляет серверные команды на клиент. Работает на серверной стороне.
  /// </summary>
  public class ServerCommandSender : NetworkBehaviour, IServerCommandSender
  {
    public void SendCommand<TDto>(ulong clientId, IServerCommand_ServerSide<TDto> command) where TDto : IDto
    {
      var rpcParams = NetworkUtils.CreateClientRpcParams(clientId);
      ExecuteCommandClientRpc(clientId, command.Key, new DtoWrapper(command.Payload), rpcParams);
    }

    [ClientRpc]
    private void ExecuteCommandClientRpc(ulong clientId, int key, DtoWrapper payload, ClientRpcParams rpcParams = default)
    {
      var serverCommandReceiver = ObjectResolversRegistry.Get(clientId).Resolve<ServerCommandReceiver>();
      serverCommandReceiver.OnCommandReceived(key, payload.Dto);
    }
  }
}