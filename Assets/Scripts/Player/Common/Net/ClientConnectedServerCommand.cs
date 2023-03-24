using Aboba.Items.Client.Services;
using Aboba.Network.Common;
using Aboba.Player.Client;
using Aboba.UI;
using VContainer;

namespace Aboba.Player.Common.Net
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

      var currentPlayerService = objectResolver.Resolve<CurrentPlayerService>();
      currentPlayerService.CurrentPlayerId = dto.ClientId;
      await objectResolver.Resolve<ClientInventoryService>().InitializeAsync();

      var screenService = objectResolver.Resolve<ScreenService>();
      screenService.Initialize();
      await screenService.OpenScreenAsync("UI/pfGameScreen");
      await objectResolver.Resolve<CameraManager>().CreateCameraAsync(currentPlayerService.Hero.transform);
    }

    public ClientConnectedServerCommand() => Payload = null!;

#endregion
  }
}