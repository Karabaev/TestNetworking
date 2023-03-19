using System.Linq;
using Aboba.Items.Descriptors;

namespace Aboba.Items
{
  public class Inventory
  {
    private readonly InventorySlot[] _slots;

    public bool AddItem(InventoryItemDescriptor itemDescriptor)
    {
      var slot = _slots.FirstOrDefault(s => s.Item == itemDescriptor);

      if(slot == null)
        return false;

      slot.AddItem(itemDescriptor);
      return true;
    }

    public void RemoveItem(int slotIndex) => _slots[slotIndex].RemoveItem();

    private InventorySlot? FindSuitableSlot(InventoryItemDescriptor itemDescriptor) => _slots.FirstOrDefault(s => s.Item == null || s.Item == itemDescriptor);

    public Inventory(int size)
    {
      _slots = new InventorySlot[size];
      for(var i = 0; i < _slots.Length; i++)
        _slots[i] = new InventorySlot();
    }
  }
}