using System.Collections.Generic;
using Aboba.Items.Common.Net;
using Aboba.Network.Common;
using Aboba.Player;
using VContainer;

namespace Aboba.Network.Server.Services
{
  /// <summary>
  /// Принимает запросы с клиента, работает на серверной стороне.
  /// </summary>
  public class ClientRequestReceiver
  {
    private readonly IObjectResolver _objectResolver;

    private readonly Dictionary<int, IClientRequest> _requestsRegistry = new();

    public IDto HandleRequest(int key, IDto payload)
    {
      var request = _requestsRegistry[key];
      return request.Execute(_objectResolver, payload);
    }
    
    public ClientRequestReceiver(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
      _requestsRegistry[GetUserInventoryClientRequest.RequestKey] = new GetUserInventoryClientRequest();
      _requestsRegistry[AttackClientRequest.RequestKey] = new AttackClientRequest();
    }
  }
}