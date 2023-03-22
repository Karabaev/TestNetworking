using Aboba.Items.Client.Services;
using Aboba.Network.Client;
using Aboba.Network.Common;
using Aboba.Player.Common.Net;
using Aboba.UI;
using VContainer;

namespace Aboba.Player.Client
{
  public class ClientConnectedServerCommand_ClientSide : IServerCommand_ClientSide
  {
    public async void Execute(IDto payload, IObjectResolver objectResolver)
    {
      var dto = (ClientConnectedDto)payload;
      
      objectResolver.Resolve<CurrentPlayerService>().CurrentPlayerId = dto.ClientId;
      await objectResolver.Resolve<ClientInventoryService>().InitializeAsync();
      
      var screenService = objectResolver.Resolve<ScreenService>();
      screenService.Initialize();
      await screenService.OpenScreenAsync("UI/pfGameScreen");
    }
  }
}