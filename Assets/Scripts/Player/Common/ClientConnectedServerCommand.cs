using Aboba.Items.Client.Services;
using Aboba.Network.Common;
using Aboba.Player.Common.Net;
using Aboba.UI;
using VContainer;

namespace Aboba.Player.Common
{
  public class ClientConnectedServerCommand : IServerCommand
  {
#region Server

    public const int CommandKey = 2;
    
    public int Key => CommandKey;

    public IDto Payload { get; }

    public ClientConnectedServerCommand(ulong clientId) => Payload = new ClientConnectedDto(clientId);

#endregion

#region Client

    public async void Execute(IDto payload, IObjectResolver objectResolver)
    {
      var dto = (ClientConnectedDto)payload;

      objectResolver.Resolve<CurrentPlayerService>().CurrentPlayerId = dto.ClientId;
      await objectResolver.Resolve<ClientInventoryService>().InitializeAsync();

      var screenService = objectResolver.Resolve<ScreenService>();
      screenService.Initialize();
      await screenService.OpenScreenAsync("UI/pfGameScreen");
    }

    public ClientConnectedServerCommand() => Payload = null!;

#endregion
  }
}