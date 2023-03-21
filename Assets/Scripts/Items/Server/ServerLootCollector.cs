using Aboba.Items.Client;
using Aboba.Items.Server.Services;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Items.Server
{
  public class ServerLootCollector : NetworkBehaviour
  {
    [Inject]
    private ServerLootService _serverLootService = null!;
    
    private void Awake() => enabled = false;

    public override void OnNetworkSpawn() => enabled = IsServer;

    private void OnTriggerEnter(Collider other)
    {
      if(!other.TryGetComponent<ClientLootObject>(out var loot))
        return;

      _serverLootService.CollectLoot(OwnerClientId, loot);
    }
  }
}