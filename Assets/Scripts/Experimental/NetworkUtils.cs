using Unity.Netcode;
using VContainer;
using VContainer.Unity;

namespace Aboba.Experimental
{
  public static class NetworkUtils
  {
    public static void Spawn(this NetworkObject obj, IObjectResolver resolver, bool destroyWithScene = false)
    {
      resolver.InjectGameObject(obj.gameObject);
      obj.Spawn(destroyWithScene);
    }
    
    public static void Spawn(this NetworkObject obj, IObjectResolver resolver, ulong ownerId, bool destroyWithScene = false)
    {
      resolver.InjectGameObject(obj.gameObject);
      obj.SpawnWithOwnership(ownerId, destroyWithScene);
    }
  }
}