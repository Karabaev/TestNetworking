using System;
using Unity.Netcode;

namespace Aboba.Infrastructure
{
  public class NetworkHooks : NetworkBehaviour
  {
    public event Action? NetworkSpawnHook;

    public event Action? NetworkDespawnHook;

    public override void OnNetworkSpawn() => NetworkSpawnHook?.Invoke();

    public override void OnNetworkDespawn() => NetworkDespawnHook?.Invoke();
  }
}