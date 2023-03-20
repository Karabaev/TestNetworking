using System.Collections.Generic;
using Aboba.Items.Descriptors;

namespace Aboba.Items
{
  public class InventoryService
  {
    private const int InventorySize = 10;

    private readonly Dictionary<ulong, Inventory> _inventories = new(2);

    public Inventory GetInventory(ulong ownerId) => _inventories[ownerId];

    public bool AddItem(ulong ownerId, InventoryItemDescriptor itemDescriptor) => _inventories[ownerId].AddItem(itemDescriptor);

    public void RemoveItem(ulong ownerId, int slotIndex) => _inventories[ownerId].RemoveItem(slotIndex);

    public void AddInventory(ulong ownerId) => _inventories.Add(ownerId, new Inventory(InventorySize));
  }
}