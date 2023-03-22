using System.Collections.Generic;
using System.Linq;
using Aboba.Experimental;
using Aboba.Infrastructure;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Server.Services;
using Aboba.Network.Server.Services;
using Aboba.Player.Server;
using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aboba
{
  [RequireComponent(typeof(PlayerInput))]
  [RequireComponent(typeof(NetworkHooks))]
  public class ServerGameController : LifetimeScope
  {
    [SerializeField]
    private NetworkObject _heroPrefab = null!;
    [SerializeField]
    private ItemsReference _itemsReference = null!;
    [SerializeField]
    private ClientGameController _clientControllerPrefab = null!;
    
    [SerializeField, HideInInspector]
    private PlayerInput _playerInput = null!;
    [SerializeField, HideInInspector]
    private NetworkHooks _networkHooks = null!;
    [SerializeField, HideInInspector]
    private NetworkObjectPool _networkObjectPool = null!;
    [SerializeField, HideInInspector]
    private ServerCommandSender _serverCommandManager = null!;

    [SerializeField, HideInInspector]
    private List<GameObject> _networkPrefabs = null!;


    protected override void Awake()
    {
      base.Awake();
      ObjectResolversRegistry.ServerObjectResolver = Container;
      _networkHooks.NetworkSpawnHook += OnNetworkSpawned;
      _networkHooks.NetworkDespawnHook += OnNetworkDespawned;
    }
    
    protected override void Configure(IContainerBuilder builder)
    {
      // server dependencies
      builder.RegisterComponent(_playerInput);
      builder.RegisterComponent(FindObjectOfType<NetworkManager>());
      builder.RegisterComponent(_networkObjectPool);
      builder.RegisterComponent(_serverCommandManager).As<IServerCommandSender>();
      builder.Register<ServerLootService>(Lifetime.Singleton);
      builder.Register<ResourceService>(Lifetime.Singleton);
      builder.Register<FromResourceFactory>(Lifetime.Singleton);
      builder.Register<ServerInventoryService>(Lifetime.Singleton);
      builder.Register<ClientRequestReceiver>(Lifetime.Singleton);
      builder.RegisterInstance(_itemsReference);
    }

    private void OnNetworkSpawned()
    {
      var networkManager = Container.Resolve<NetworkManager>();
      
      if(!networkManager.IsServer)
        return;

      networkManager.OnClientConnectedCallback += OnClientConnected;
      networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnNetworkDespawned() { }
    
    private void OnClientConnected(ulong clientId)
    {
      var spawnPoint = Vector3.zero;
      
      var hero = Instantiate(_heroPrefab, spawnPoint, Quaternion.identity);
      hero.SpawnWithOwnership(clientId, true);

      Container.Resolve<ServerInventoryService>().AddInventory(clientId);
      
      var controller = Instantiate(_clientControllerPrefab);
      controller.name = $"ClientController_{clientId}";
      controller.RequireComponent<NetworkObject>().SpawnWithOwnership(clientId, true);
      controller.transform.AddChild(hero);

      ObjectResolversRegistry.Add(clientId, controller.Container);
      
      Container.Resolve<ServerCommandSender>().SendCommand(clientId, new ClientConnectedServerCommand_ServerSide(clientId));
    }
    
    private void OnClientDisconnected(ulong clientId)
    {
      var networkManager = Container.Resolve<NetworkManager>();
      
      foreach(var obj in networkManager.SpawnManager.GetClientOwnedObjects(clientId))
        obj.Despawn();

      ObjectResolversRegistry.Remove(clientId);
    }

    private void OnValidate()
    {
      _playerInput = this.RequireComponent<PlayerInput>();
      _networkHooks = this.RequireComponent<NetworkHooks>();
      _networkObjectPool = this.RequireComponent<NetworkObjectPool>();
      _serverCommandManager = this.RequireComponent<ServerCommandSender>();

      _networkPrefabs = new List<GameObject>();

      var networkConfig = FindObjectOfType<NetworkManager>().NetworkConfig;
      var prefabsInfos = ReflectionUtils.RequireCollectionValueOfPrivateField(networkConfig, "NetworkPrefabs");

      foreach(var prefabInfo in prefabsInfos)
      {
        var prefab = ReflectionUtils.RequireValueOfPublicField<GameObject>(prefabInfo, "Prefab");
        _networkPrefabs.Add(prefab!);
      }

      autoInjectGameObjects = FindObjectsOfType<MonoBehaviour>().Select(c => c.gameObject).ToList();
    }
  }
}