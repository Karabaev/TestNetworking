using Aboba.Items.Descriptors;

namespace Aboba.Items
{
  public class InventorySlot
  {
    public InventoryItemDescriptor? Item { get; private set; }
    
    public int Count { get; private set; }

    public void AddItem(InventoryItemDescriptor item)
    {
      Item = item;
      Count++;
    }

    public void RemoveItem()
    {
      Item = null;
      Count = 0;
    }
  }
}