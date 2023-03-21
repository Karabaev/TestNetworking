using System;
using Aboba.Items.Common.Net.Dto;
using Aboba.Items.Server.Services;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using VContainer;

namespace Aboba.Network.Client
{
  /// <summary>
  /// Работает на клиенте в контексте игрока.
  /// </summary>
  public class ClientRequestManager : NetworkBehaviour, IRequestManager
  {
    [Inject]
    private ServerInventoryService _serverInventoryService = null!;
    
    private UniTaskCompletionSource<InventoryDto> _taskCompletionSource = null!;

    public UniTask<InventoryDto> RequestUserInventoryAsync()
    {
      if(!IsClient || !IsOwner)
        throw new Exception();

      _taskCompletionSource = new UniTaskCompletionSource<InventoryDto>();
      RequestServerRpc();
      return _taskCompletionSource.Task;
    }

    [ServerRpc]
    private void RequestServerRpc()
    {
      if(!NetworkManager.ConnectedClients.ContainsKey(OwnerClientId))
        return;

      var inventory = _serverInventoryService.GetInventory(OwnerClientId);
      var dto = new InventoryDto(inventory);
      
      ResponseClientRpc(dto, NetworkUtils.CreateClientRpcParams(OwnerClientId));
    }
    
    [ClientRpc]
    private void ResponseClientRpc(InventoryDto payload, ClientRpcParams rpcParams)
    {
      _taskCompletionSource.TrySetResult(payload);
    }
    
    // [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = true)]
    // private void RequestServerRpc(ServerRpcParams rpcParams = default)
    // {
    //   var clientId = rpcParams.Receive.SenderClientId;
    //   if(!NetworkManager.ConnectedClients.ContainsKey(clientId))
    //     return;
    //   
    //   
    //
    //   request.GenerateServerResponse();
    //
    //   ResponseClientRpc();
    // }
    
    // public UniTask<T> SendRequestAsync<T>(IClientRequest<T> request) where T : IServerResponse
    // {
    //   if(!IsClient)
    //     throw new Exception();
    //
    //   _taskCompletionSource = new UniTaskCompletionSource();
    //   
    //   RequestServerRpc();
    //   
    //   var res = request.GenerateServerResponse();
    //
    //   return _taskCompletionSource.Task;
    // }
  }
}