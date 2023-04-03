using Unity.Netcode;

namespace Aboba.Network.Common
{
  public interface IDto : INetworkSerializable
  {
    DtoType Type { get; }
    
    public enum DtoType
    {
      Dummy = -1,
      AddedInventoryItemDto = 0,
      ClientConnected = 1,
      GetUserInventory = 2,
      GetUserInventoryResponse = 3,
      Attack = 4
    }
  }
}