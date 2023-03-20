using System;
using System.Collections.Generic;
using System.Linq;
using Aboba.Items.Descriptors;

namespace Aboba.Items
{
  public class Inventory
  {
    private readonly InventorySlot[] _slots;

    public IReadOnlyList<InventorySlot> Slots => _slots;

    public event Action<int>? ItemAdded;
    
    public event Action<int>? ItemRemoved;
    
    public bool AddItem(InventoryItemDescriptor itemDescriptor)
    {
      var slot = FindSuitableSlot(itemDescriptor);

      if(slot == null)
        return false;

      slot.AddItem(itemDescriptor);
      ItemAdded?.Invoke(slot.Index);
      return true;
    }

    public void RemoveItem(int slotIndex)
    {
      _slots[slotIndex].RemoveItem();
      ItemRemoved?.Invoke(slotIndex);
    }

    private InventorySlot? FindSuitableSlot(InventoryItemDescriptor itemDescriptor) => _slots.FirstOrDefault(s => s.Item == null || s.Item == itemDescriptor);

    public Inventory(int size)
    {
      _slots = new InventorySlot[size];
      for(var i = 0; i < _slots.Length; i++)
        _slots[i] = new InventorySlot(i);
    }
  }
}