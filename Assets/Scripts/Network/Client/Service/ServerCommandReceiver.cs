using System.Collections.Generic;
using Aboba.Items.Client.Net;
using Aboba.Items.Server.Net;
using Aboba.Network.Common;
using Aboba.Player.Client;
using Aboba.Player.Server;

namespace Aboba.Network.Client.Service
{
  /// <summary>
  /// Принимает команды с сервера. Работает на клиентской стороне. Работает в контексте игрока.
  /// </summary>
  public class ServerCommandReceiver
  {
    private readonly Dictionary<int, IServerCommand_ClientSide> _commandsRegistry = new();

    public void OnCommandReceived<TDto>(int key, TDto payload) where TDto : IDto
    {
      _commandsRegistry[key].Execute(payload, ObjectResolversRegistry.LocalObjectResolver);
    }

    public ServerCommandReceiver()
    {
      _commandsRegistry[AddedInventoryItemServerCommand_ServerSide.CommandKey] = new AddedInventoryItemServerCommand_ClientSide();
      _commandsRegistry[ClientConnectedServerCommand_ServerSide.CommandKey] = new ClientConnectedServerCommand_ClientSide();
    }
  }
}