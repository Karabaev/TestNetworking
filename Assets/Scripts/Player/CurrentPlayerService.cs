using Aboba.Characters;
using Aboba.Utils;
using JetBrains.Annotations;
using Unity.Netcode;

namespace Aboba.Player
{
  [UsedImplicitly]
  public class CurrentPlayerService
  {
    private readonly NetworkManager _networkManager;
    
    public ulong CurrentPlayerId { get; set; }

    public CharacterComponent Hero => _networkManager.SpawnManager.GetLocalPlayerObject().RequireComponent<CharacterComponent>();

    public CurrentPlayerService(NetworkManager networkManager) => _networkManager = networkManager;
  }
}