using System.Collections.Generic;
using Aboba.Items.Common.Net;
using Aboba.Network.Common;
using Aboba.Player.Common;
using Aboba.Player.Common.Net;

namespace Aboba.Network.Client.Service
{
  /// <summary>
  /// Принимает команды с сервера. Работает на клиентской стороне. Работает в контексте игрока.
  /// </summary>
  public class ServerCommandReceiver
  {
    private readonly Dictionary<int, IServerCommand> _commandsRegistry = new();

    public void OnCommandReceived(int key, IDto payload)
    {
      _commandsRegistry[key].Execute(payload, ObjectResolversRegistry.LocalObjectResolver);
    }

    public ServerCommandReceiver()
    {
      _commandsRegistry[AddedInventoryItemServerCommand.CommandKey] = new AddedInventoryItemServerCommand();
      _commandsRegistry[ClientConnectedServerCommand.CommandKey] = new ClientConnectedServerCommand();
    }
  }
}