using System.Collections.Generic;
using Aboba.Items.Client.Net;
using Aboba.Items.Server.Net;
using Aboba.Network.Common;
using VContainer;

namespace Aboba.Network.Server.Services
{
  /// <summary>
  /// Принимает запросы с клиента, работает на серверной стороне.
  /// </summary>
  public class ClientRequestReceiver
  {
    private readonly IObjectResolver _objectResolver;

    private readonly Dictionary<int, IClientRequest_ServerSide> _requestsRegistry = new();

    public IDto HandleRequest(int key, IDto payload)
    {
      var request = _requestsRegistry[key];
      return request.Execute(_objectResolver, payload);
    }
    
    public ClientRequestReceiver(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
      _requestsRegistry[GetUserInventoryClientRequest_ClientSide.RequestKey] = new GetUserInventoryClientRequest_ServerSide();
    }
  }
}