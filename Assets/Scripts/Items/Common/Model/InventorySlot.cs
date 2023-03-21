using Aboba.Items.Common.Descriptors;

namespace Aboba.Items.Common.Model
{
  public class InventorySlot
  {
    public int Index { get; }
    
    public InventoryItemDescriptor? Item { get; private set; }
    
    public int Count { get; private set; }

    public void AddItems(InventoryItemDescriptor item, int count)
    {
      Item = item;
      Count += count;
    }

    public void RemoveItem()
    {
      Item = null;
      Count = 0;
    }

    public InventorySlot(int index) => Index = index;
  }
}