using Aboba.Infrastructure;
using Aboba.Items.Client.Services;
using Aboba.Network.Client.Service;
using Aboba.Player;
using Aboba.Player.Client;
using Aboba.UI;
using Aboba.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aboba
{
  public class ClientGameController : LifetimeScope 
  {
    [SerializeField, HideInInspector]
    private ClientRequestManager _clientRequestManager = null!;
    [SerializeField, HideInInspector]
    private NetworkHooks _networkHooks = null!;
    
    protected override void Awake()
    {
      base.Awake();
      ObjectResolversRegistry.LocalObjectResolver = Container;
      _networkHooks.NetworkSpawnHook += OnNetworkSpawned;
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
      builder.Register<ClientInventoryService>(Lifetime.Singleton);
      builder.Register<ServerCommandReceiver>(Lifetime.Singleton);
      builder.RegisterComponent(FindObjectOfType<MainCanvas>().RequireComponent<Canvas>());
      builder.Register<CameraManager>(Lifetime.Singleton);
    }

    private void OnNetworkSpawned() { }
    
    private void OnValidate()
    {
      _clientRequestManager = this.RequireComponent<ClientRequestManager>();
      _networkHooks = this.RequireComponent<NetworkHooks>();
    }
  }
}