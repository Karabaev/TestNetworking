using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Aboba.Infrastructure
{
  [UsedImplicitly]
  public class ResourceService
  {
    public async UniTask<T> LoadAsync<T>(string path) where T : Object
    {
      var loadAsync = Resources.LoadAsync<T>(path);
      var resource = await loadAsync.ToUniTask();
      return (T)resource;
    }

    public T Load<T>(string path) where T : Object => Resources.Load<T>(path);

    public void Unload(Object resource) => Resources.UnloadAsset(resource);
  }
}