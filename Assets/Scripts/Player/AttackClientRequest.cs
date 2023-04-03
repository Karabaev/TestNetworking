using Aboba.Characters;
using Aboba.Network.Common;
using Aboba.Utils;
using Unity.Netcode;
using VContainer;

namespace Aboba.Player
{
  public class AttackClientRequest : ClientRequest<AttackDto>
  {
    public const int RequestKey = 1;

#region Client
    
    public override int Key => RequestKey;
    
    public AttackClientRequest(ulong clientId) : base(new AttackDto(clientId)) { }

#endregion
    
#region Server

    public override IDto Execute(IObjectResolver objectResolver, IDto payload)
    {
      var dto = (AttackDto)payload;
      var playerHero = objectResolver.Resolve<NetworkManager>().SpawnManager.GetPlayerNetworkObject(dto.UserId);
      playerHero.RequireComponent<CharacterComponent>().Attack();
      // запустить анимацию
      // если был хит, то кинуть серверную команду о попадании
      return new DummyDto();
    }
    
    public AttackClientRequest() : base(new AttackDto()) { }
    
#endregion
  }
}