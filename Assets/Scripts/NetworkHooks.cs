using System;
using Unity.Netcode;

namespace Aboba
{
  public class NetworkHooks : NetworkBehaviour
  {
    public event Action? OnNetworkSpawnHook;

    public event Action? OnNetworkDespawnHook;

    public override void OnNetworkSpawn() => OnNetworkSpawnHook?.Invoke();

    public override void OnNetworkDespawn() => OnNetworkDespawnHook?.Invoke();
  }
}