using Aboba.Infrastructure;
using Aboba.Items.Client.Services;
using Aboba.Network.Client;
using Aboba.Network.Client.Service;
using Aboba.Player;
using Aboba.UI;
using Aboba.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aboba
{
  [RequireComponent(typeof(NetworkHooks))]
  public class ClientGameController : LifetimeScope
  {
    [SerializeField, HideInInspector]
    private NetworkHooks _networkHooks = null!;
    [SerializeField, HideInInspector]
    private ClientRequestManager _clientRequestManager = null!;
    
    protected override void Awake()
    {
      base.Awake();
      _networkHooks.OnNetworkSpawnHook += OnNetworkSpawned;
    }

    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
      builder.Register<ResourceService>(Lifetime.Singleton);
      builder.Register<FromResourceFactory>(Lifetime.Singleton);
      builder.Register<UIService>(Lifetime.Singleton);
      builder.Register<ScreenService>(Lifetime.Singleton);
      builder.Register<CurrentPlayerService>(Lifetime.Singleton);
      builder.RegisterComponent(_clientRequestManager).As<IClientRequestManager>();
      builder.Register<ServerCommandReceiver>(Lifetime.Singleton);
      builder.Register<ClientInventoryService>(Lifetime.Singleton);
      builder.RegisterComponent(FindObjectOfType<Canvas>());
    }

    private async void OnNetworkSpawned()
    {
      if(!_networkHooks.IsOwner)
        return;
      
      Container.Resolve<CurrentPlayerService>().CurrentPlayerId = _networkHooks.OwnerClientId;
      
      await Container.Resolve<ClientInventoryService>().InitializeAsync();

      var screenService = Container.Resolve<ScreenService>();
      screenService.Initialize();
      await screenService.OpenScreenAsync("UI/pfGameScreen");
    }

    private void OnValidate()
    {
      _networkHooks = this.RequireComponent<NetworkHooks>();
      _clientRequestManager = this.RequireComponent<ClientRequestManager>();
    }
  }
}