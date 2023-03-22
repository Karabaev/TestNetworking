using Aboba.Network.Common;
using Cysharp.Threading.Tasks;

namespace Aboba.Network.Client.Service
{
  public interface IClientRequestManager
  {
    UniTask<TResponse> SendRequestAsync<TDto, TResponse>(IClientRequest_ClientSide<TDto> request) where TDto : IDto where TResponse : IDto;
    
    UniTask<IDto> SendRequestAsync<TDto>(IClientRequest_ClientSide<TDto> request) where TDto : IDto;
  }
}