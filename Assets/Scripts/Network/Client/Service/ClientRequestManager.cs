using Aboba.Network.Common;
using Aboba.Network.Server.Services;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using VContainer;

namespace Aboba.Network.Client.Service
{
  /// <summary>
  /// Отправляет запросы на сервер. Работает на клиентской стороне в контексте игрока.
  /// </summary>
  public class ClientRequestManager : NetworkBehaviour, IClientRequestManager
  {
    private UniTaskCompletionSource<IDto> _taskCompletionSource = null!;

    public async UniTask<TResponse> SendRequestAsync<TDto, TResponse>(ClientRequest<TDto> request) where TDto : IDto where TResponse : IDto
    {
      var result = await SendRequestAsync(request);
      return (TResponse)result;
    }

    public UniTask<IDto> SendRequestAsync<TDto>(ClientRequest<TDto> request) where TDto : IDto
    {
      _taskCompletionSource = new UniTaskCompletionSource<IDto>();
      HandleRequestServerRpc(request.Key, new DtoWrapper(request.Payload));
      return _taskCompletionSource.Task;
    }

    [ServerRpc]
    private void HandleRequestServerRpc(int key, DtoWrapper payload, ServerRpcParams rpcParams = default)
    {
      var clientRequestReceiver = ObjectResolversRegistry.ServerObjectResolver.Resolve<ClientRequestReceiver>();
      var response = clientRequestReceiver.HandleRequest(key, payload.Dto);
      var clientRpcParams = NetworkUtils.CreateClientRpcParams(rpcParams.Receive.SenderClientId);
      ResponseClientRpc(new DtoWrapper(response), clientRpcParams);
    }

    [ClientRpc]
    private void ResponseClientRpc(DtoWrapper response, ClientRpcParams rpcParams)
    {
      _taskCompletionSource.TrySetResult(response.Dto);
    }
  }
}