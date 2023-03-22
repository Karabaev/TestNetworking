using Aboba.Network.Common;
using Unity.Netcode;

namespace Aboba.Items.Common.Net.Dto
{
  public struct GetUserInventoryDto : IDto
  {
    private ulong _userId;
    
    public IDto.DtoType Type => IDto.DtoType.GetUserInventory;

    public ulong UserId => _userId;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref _userId);
    }

    public GetUserInventoryDto(ulong userId) => _userId = userId;
  }
}