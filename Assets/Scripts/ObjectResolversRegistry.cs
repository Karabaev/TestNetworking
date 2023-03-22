using System.Collections.Generic;
using Aboba.Utils;
using VContainer;

namespace Aboba
{
  public static class ObjectResolversRegistry
  {
    private static readonly Dictionary<ulong, IObjectResolver> _clientResolvers = new();

    /// <summary>
    /// Серверный резолвер. Инициализируется сервером.
    /// </summary>
    public static IObjectResolver ServerObjectResolver { get; set; } = null!;
    
    /// <summary>
    /// Клиентский резолвер. Инициализируется клиентом.
    /// </summary>
    public static IObjectResolver LocalObjectResolver { get; set; } = null!;
    
    /// <summary>
    /// Возвращает клиентский резовлер для указанного клиента. Вызывается на сервере.
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public static IObjectResolver Get(ulong clientId) => _clientResolvers.Require(clientId);
    
    /// <summary>
    /// Добавляет резолвер в коллекцию резолверов с привязкой к клиенту. Вызывается на сервере.
    /// </summary>
    public static void Add(ulong clientId, IObjectResolver resolver) => _clientResolvers[clientId] = resolver;

    public static void Remove(ulong clientId)
    {
      _clientResolvers[clientId].Dispose();
      _clientResolvers.Remove(clientId);
    }
  }
}