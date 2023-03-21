using System.Collections.Generic;
using System.Linq;
using Aboba.Experimental;
using Aboba.Infrastructure;
using Aboba.Items.Client.Services;
using Aboba.Items.Common.Descriptors;
using Aboba.Items.Server.Services;
using Aboba.Network.Client;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Aboba
{
  [RequireComponent(typeof(PlayerInput))]
  [RequireComponent(typeof(NetworkHooks))]
  public class GameServerController : LifetimeScope
  {
    [SerializeField]
    private NetworkObject _heroPrefab = null!;
    [SerializeField]
    private ItemsReference _itemsReference = null!;
    
    [SerializeField, HideInInspector]
    private PlayerInput _playerInput = null!;
    [SerializeField, HideInInspector]
    private NetworkHooks _networkHooks = null!;
    [SerializeField, HideInInspector]
    private NetworkObjectPool _networkObjectPool = null!;
    [SerializeField, HideInInspector]
    private List<GameObject> _networkPrefabs = null!;
    [SerializeField, HideInInspector]
    private ClientRequestManager _clientRequestManager = null!; 

    protected override void Awake()
    {
      base.Awake();
      _networkHooks.OnNetworkSpawnHook += OnNetworkSpawned;
      _networkHooks.OnNetworkDespawnHook += OnNetworkDespawned;
    }
    
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterComponent(_playerInput);
      builder.RegisterComponent(FindObjectOfType<NetworkManager>());
      builder.RegisterComponent(_networkObjectPool);
      builder.Register<ServerLootService>(Lifetime.Singleton);
      builder.Register<CurrentPlayerService>(Lifetime.Singleton);
      builder.Register<ResourceService>(Lifetime.Singleton);
      builder.Register<FromResourceFactory>(Lifetime.Singleton);
      builder.Register<ServerInventoryService>(Lifetime.Singleton);
      builder.Register<ClientInventoryService>(Lifetime.Singleton);
      builder.RegisterComponent(_clientRequestManager).As<IRequestManager>();
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
    
    private void OnNetworkDespawned()
    {
    }
    
    private void OnClientConnected(ulong clientId)
    {
      var networkManager = Container.Resolve<NetworkManager>();

      if(_networkHooks.IsOwner)
      {
        Container.Resolve<CurrentPlayerService>().CurrentPlayerId = clientId;
      }

      if(networkManager.IsServer)
      {
        var spawnPoint = Vector3.zero;
      
        var hero = Instantiate(_heroPrefab, spawnPoint, Quaternion.identity);
        hero.SpawnWithOwnership(clientId, true);

        Container.Resolve<ServerInventoryService>().AddInventory(clientId);
      }

      if(networkManager.IsClient)
      {
        Container.Resolve<ClientInventoryService>().InitializeAsync().Forget();
      }
    }
    
    private void OnClientDisconnected(ulong clientId)
    {
      var networkManager = Container.Resolve<NetworkManager>();
      
      foreach(var obj in networkManager.SpawnManager.GetClientOwnedObjects(clientId))
        obj.Despawn();
    }

    private void OnValidate()
    {
      _playerInput = this.RequireComponent<PlayerInput>();
      _networkHooks = this.RequireComponent<NetworkHooks>();
      _networkObjectPool = this.RequireComponent<NetworkObjectPool>();
      _clientRequestManager = this.RequireComponent<ClientRequestManager>();

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