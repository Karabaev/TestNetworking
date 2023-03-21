using Unity.Netcode;

namespace Aboba.Items.Common.Net.Dto
{
  public struct InventorySlotDto : INetworkSerializable
  {
    private int _inventoryIndex;
    private string _itemId;
    private int _count;

    public int InventoryIndex => _inventoryIndex;

    public string? ItemId => _itemId;

    public int Count => _count;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref _inventoryIndex);
      serializer.SerializeValue(ref _itemId);
      serializer.SerializeValue(ref _count);
    }

    public InventorySlotDto(int inventoryIndex, string? itemId, int count)
    {
      _inventoryIndex = inventoryIndex;
      _itemId = itemId ?? string.Empty;
      _count = count;
    }
  }
}