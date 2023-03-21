using Aboba.Items.Client.Services;
using Aboba.Network.Client;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using VContainer;

namespace Aboba
{
  public class ClientGameController : NetworkBehaviour
  {
    [Inject]
    private CurrentPlayerService _currentPlayerService = null!;
    [Inject]
    private ClientInventoryService _clientInventoryService = null!;

    public override void OnNetworkSpawn()
    {
      if(!IsOwner)
        return;
      
      _currentPlayerService.CurrentPlayerId = OwnerClientId;
      _clientInventoryService.InitializeAsync().Forget();
    }
  }
}