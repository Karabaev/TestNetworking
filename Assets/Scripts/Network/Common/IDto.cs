using Unity.Netcode;

namespace Aboba.Network.Common
{
  public interface IDto : INetworkSerializable
  {
    DtoType Type { get; }
    
    public enum DtoType
    {
      AddedInventoryItemDto = 0
    }
  }
}