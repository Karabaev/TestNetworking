using System.Collections.Generic;
using Aboba.Items.Common.Model;
using Aboba.Utils;
using Unity.Netcode;

namespace Aboba.Items.Common.Net.Dto
{
  public struct InventoryDto : INetworkSerializable
  {
    private InventorySlotDto[] _slots;

    public IReadOnlyList<InventorySlotDto> Slots => _slots;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      var length = 0;

      if(!serializer.IsReader)
        length = _slots.Length;

      serializer.SerializeValue(ref length);

      if(serializer.IsReader)
        _slots = new InventorySlotDto[length];

      for(var i = 0; i < length; i++)
        serializer.SerializeValue(ref _slots[i]);
    }

    public InventoryDto(InventorySlotDto[] slots) => _slots = slots;

    public InventoryDto(Inventory source)
    {
      _slots = new InventorySlotDto[source.Slots.Count];

      for(var i = 0; i < source.Slots.Count; i++)
      {
        var slot = source.Slots[i];
        _slots[i] = new InventorySlotDto(slot.Index, slot.Item.IsNull()?.Id, slot.Count);
      }
    }
  }
}