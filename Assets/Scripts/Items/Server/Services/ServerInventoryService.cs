using System.Collections.Generic;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Common.Model;
using Aboba.Network.Server;

namespace Aboba.Items.Server.Services
{
  public class ServerInventoryService
  {
    private const int InventorySize = 10;

    private readonly ServerCommandManager _serverCommandManager;

    private readonly Dictionary<ulong, Inventory> _inventories = new(2);

    public Inventory GetInventory(ulong ownerId) => _inventories[ownerId];

    public bool AddItem(ulong ownerId, InventoryItemDescriptor itemDescriptor)
    {
      var result = _inventories[ownerId].AddItems(itemDescriptor);

      if(result)
      {
        _serverCommandManager.NotifyInventoryItemAdded(ownerId, itemDescriptor.Id, 1);
      }

      return result;
    }

    public void RemoveItem(ulong ownerId, int slotIndex)
    {
      _inventories[ownerId].RemoveItem(slotIndex);
    }

    public void AddInventory(ulong ownerId) => _inventories.Add(ownerId, new Inventory(InventorySize));

    public ServerInventoryService(ServerCommandManager serverCommandManager) => _serverCommandManager = serverCommandManager;
  }
}