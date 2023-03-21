using System;
using System.Collections.Generic;
using System.Linq;
using Aboba.Items.Common.Descriptors;

namespace Aboba.Items.Common.Model
{
  public class Inventory
  {
    private readonly InventorySlot[] _slots;

    public IReadOnlyList<InventorySlot> Slots => _slots;

    public event Action<int>? ItemAdded;
    
    public event Action<int>? ItemRemoved;
    
    public bool AddItems(InventoryItemDescriptor itemDescriptor, int count = 1)
    {
      var slot = FindSuitableSlot(itemDescriptor);
      
      if(slot == null)
        return false;
      
      slot.AddItems(itemDescriptor, count);
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