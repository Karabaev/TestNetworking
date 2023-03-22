using Aboba.Items.Common.Net.Dto;
using Cysharp.Threading.Tasks;

namespace Aboba.Network.Client.Service
{
  public interface IClientRequestManager
  {
    UniTask<InventoryDto> RequestUserInventoryAsync();
  }
}