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
    public void SendCommand<TDto>(ulong clientId, IServerCommand_ServerSide<TDto> command) where TDto : IDto
    {
      var rpcParams = NetworkUtils.CreateClientRpcParams(clientId);
      ExecuteCommandClientRpc(command.Key, new DtoWrapper(command.Payload), rpcParams);
    }
    
    [UsedImplicitly]
    [ClientRpc]
    private void ExecuteCommandClientRpc(int key, DtoWrapper payload, ClientRpcParams rpcParams = default)
    {
      var localObjectResolver = ObjectResolversRegistry.LocalObjectResolver;
      var serverCommandReceiver = localObjectResolver.Resolve<ServerCommandReceiver>();
      serverCommandReceiver.OnCommandReceived(key, payload.Dto);
    }
  }
}