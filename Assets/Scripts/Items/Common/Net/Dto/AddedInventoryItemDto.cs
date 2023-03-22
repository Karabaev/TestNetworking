using Aboba.Network.Common;
using Unity.Netcode;

namespace Aboba.Items.Common.Net.Dto
{
  public struct AddedInventoryItemDto : IDto
  {
    private string _itemId;
    private int _count;

    public string ItemId => _itemId;

    public int Count => _count;
    
    public IDto.DtoType Type => IDto.DtoType.AddedInventoryItemDto;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref _itemId);
      serializer.SerializeValue(ref _count);
    }

    public AddedInventoryItemDto(string itemId, int count)
    {
      _itemId = itemId;
      _count = count;
    }
  }
}