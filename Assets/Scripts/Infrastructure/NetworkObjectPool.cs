using System.Collections.Generic;
using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.Infrastructure
{
  public class NetworkObjectPool : NetworkBehaviour
  {
    private readonly Dictionary<GameObject, Queue<NetworkObject>> _pooledObjects = new();
    
    public NetworkObject Get(GameObject prefab)
    {
      return Get(prefab, Vector3.zero, Quaternion.identity);
    }
    
    public NetworkObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
      if(!_pooledObjects.TryGetValue(prefab, out var collection))
      {
        collection = new Queue<NetworkObject>(4);
        _pooledObjects[prefab] = collection;
      }

      if(!collection.TryDequeue(out var result))
        result = Instantiate(prefab).GetComponent<NetworkObject>();

      result.SetActive(true);
      result.transform.position = position;
      result.transform.rotation = rotation;

      return result;
    }

    public void Return(NetworkObject networkObject, GameObject prefab)
    {
      var go = networkObject.gameObject;
      go.SetActive(false);
      _pooledObjects[prefab].Enqueue(networkObject);
    }
  }
}