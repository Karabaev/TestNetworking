using System.Collections.Generic;
using Aboba.Items.Client.Net;
using Aboba.Items.Server.Net;
using Aboba.Network.Common;
using VContainer;

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
      _commandsRegistry[key].Execute(payload);
    }

    public ServerCommandReceiver(IObjectResolver objectResolver)
    {
      _commandsRegistry[AddedInventoryItemServerCommand_ServerSide.CommandKey] = new AddedInventoryItemServerCommand_ClientSide(objectResolver);
    }
  }
}