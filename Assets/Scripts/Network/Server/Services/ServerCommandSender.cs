using Aboba.Network.Client.Service;
using Aboba.Network.Common;
using JetBrains.Annotations;
using Unity.Netcode;
using VContainer;

namespace Aboba.Network.Server.Services
{
  /// <summary>
  /// Отправляет серверные команды на клиент. Работает на серверной стороне.
  /// </summary>
  public class ServerCommandSender : NetworkBehaviour, IServerCommandSender
  {
    /// <summary>
    /// Отправляет команду указанному клиенту.
    /// </summary>
    public void SendCommand(ulong clientId, IServerCommand command)
    {
      var rpcParams = NetworkUtils.CreateClientRpcParams(clientId);
      ExecuteCommandClientRpc(command.Key, new DtoWrapper(command.Payload), rpcParams);
    }
    
    [UsedImplicitly]
    [ClientRpc]
    private void ExecuteCommandClientRpc(int key, DtoWrapper payload, ClientRpcParams rpcParams = default)
    {
      var commandReceiver = ObjectResolversRegistry.LocalObjectResolver.Resolve<ServerCommandReceiver>();
      commandReceiver.OnCommandReceived(key, payload.Dto);
    }
  }
}