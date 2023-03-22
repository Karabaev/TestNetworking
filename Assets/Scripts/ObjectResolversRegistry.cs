using System.Collections.Generic;
using Aboba.Utils;
using VContainer;

namespace Aboba
{
  public static class ObjectResolversRegistry
  {
    private static readonly Dictionary<ulong, IObjectResolver> _clientResolvers = new();

    public static IObjectResolver Get(ulong clientId) => _clientResolvers.Require(clientId);
    
    public static void Add(ulong clientId, IObjectResolver resolver) => _clientResolvers[clientId] = resolver;

    public static void Remove(ulong clientId)
    {
      _clientResolvers[clientId].Dispose();
      _clientResolvers.Remove(clientId);
    }
  }
}