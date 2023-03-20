using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aboba.Experimental
{
  public class InjectablePrefabInstanceHandler : INetworkPrefabInstanceHandler
  {
    private readonly IObjectResolver _objectResolver;
    private readonly NetworkObjectPool _pool;
    private readonly GameObject _prefab;
    
    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
      var result = _pool.Get(_prefab, position, rotation);
      _objectResolver.InjectGameObject(result.gameObject);
      return result;
    }

    public void Destroy(NetworkObject networkObject) => _pool.Return(networkObject, _prefab);
    
    public InjectablePrefabInstanceHandler(IObjectResolver objectResolver, NetworkObjectPool pool, GameObject prefab)
    {
      _objectResolver = objectResolver;
      _pool = pool;
      _prefab = prefab;
    }
  }
}