using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Experimental
{
  public class PrefabInstanceHandlerService
  {
    private readonly NetworkManager _networkManager;
    private readonly IObjectResolver _objectResolver;
    private readonly NetworkObjectPool _networkObjectPool;
    
    public void AddHandlers(IReadOnlyList<GameObject> networkPrefabs)
    {
      foreach(var networkPrefab in networkPrefabs)
        _networkManager.PrefabHandler.AddHandler(networkPrefab, new InjectablePrefabInstanceHandler(_objectResolver, _networkObjectPool, networkPrefab));
    }
    
    public void RemoveHandlers(IReadOnlyList<GameObject> networkPrefabs)
    {
      foreach(var networkPrefab in networkPrefabs)
        _networkManager.PrefabHandler.RemoveHandler(networkPrefab);
    }
    
    
  }
}