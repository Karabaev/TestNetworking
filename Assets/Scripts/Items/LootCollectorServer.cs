using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Items
{
  public class LootCollectorServer : NetworkBehaviour
  {
    [Inject]
    private LootService _lootService = null!;
    
    private void Awake() => enabled = false;

    public override void OnNetworkSpawn() => enabled = IsServer;

    private void OnTriggerEnter(Collider other)
    {
      if(!other.TryGetComponent<ClientLootObject>(out var loot))
        return;

      _lootService.CollectLoot(OwnerClientId, loot);
    }
  }
}