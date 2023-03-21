using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client;
using Unity.Netcode;
using UnityEngine.Assertions;
using VContainer;

namespace Aboba.Network.Server
{
  public class ServerCommandManager : NetworkBehaviour, IServerCommandManager
  {
    [Inject]
    private readonly ServerCommandReceiver _serverCommandReceiver = null!;
    
    public void NotifyInventoryItemAdded(ulong clientId, string itemId, int count)
    {
      Assert.IsTrue(IsServer);
      
      ClientRpc(new AddedInventoryItemDto(itemId, count), NetworkUtils.CreateClientRpcParams(clientId));
    }

    [ClientRpc]
    private void ClientRpc(AddedInventoryItemDto dto, ClientRpcParams rpcParams = default)
    {
      _serverCommandReceiver.OnItemsAdded(dto);
    }
  }
}