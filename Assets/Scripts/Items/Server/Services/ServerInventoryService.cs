using System.Collections.Generic;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Common.Model;
using Aboba.Items.Server.Net;
using Aboba.Network.Server;
using Aboba.Network.Server.Services;

namespace Aboba.Items.Server.Services
{
  public class ServerInventoryService
  {
    private const int InventorySize = 10;

    private readonly IServerCommandSender _serverCommandSender;

    private readonly Dictionary<ulong, Inventory> _inventories = new(2);

    public Inventory GetInventory(ulong ownerId) => _inventories[ownerId];

    public bool AddItem(ulong ownerId, InventoryItemDescriptor itemDescriptor)
    {
      var result = _inventories[ownerId].AddItems(itemDescriptor);

      if(result)
      {
        var command = new AddedInventoryItemServerCommand_ServerSide(itemDescriptor.Id, 1);
        _serverCommandSender.SendCommand(ownerId, command);
      }

      return result;
    }

    public void RemoveItem(ulong ownerId, int slotIndex)
    {
      _inventories[ownerId].RemoveItem(slotIndex);
    }

    public void AddInventory(ulong ownerId) => _inventories.Add(ownerId, new Inventory(InventorySize));

    public ServerInventoryService(IServerCommandSender serverCommandSender) => _serverCommandSender = serverCommandSender;
  }
}