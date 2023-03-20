using System.Collections.Generic;
using Aboba.Experimental;
using Aboba.Infrastructure;
using Aboba.Items;
using Aboba.Utils;
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
    
    [SerializeField, HideInInspector]
    private PlayerInput _playerInput = null!;
    [SerializeField, HideInInspector]
    private NetworkHooks _networkHooks = null!;
    [SerializeField, HideInInspector]
    private NetworkObjectPool _networkObjectPool = null!;
    [SerializeField, HideInInspector]
    private List<GameObject> _networkPrefabs = null!;

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
      builder.Register<InventoryService>(Lifetime.Singleton);
      builder.Register<LootService>(Lifetime.Singleton);
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

      if(!networkManager.IsServer)
        return;

      var spawnPoint = Vector3.zero;
      
      var hero = Instantiate(_heroPrefab, spawnPoint, Quaternion.identity);
      hero.SpawnWithOwnership(clientId, true);

      Container.Resolve<InventoryService>().AddInventory(clientId);
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

      _networkPrefabs = new List<GameObject>();

      var networkConfig = FindObjectOfType<NetworkManager>().NetworkConfig;
      var prefabsInfos = ReflectionUtils.RequireCollectionValueOfPrivateField(networkConfig, "NetworkPrefabs");

      foreach(var prefabInfo in prefabsInfos)
      {
        var prefab = ReflectionUtils.RequireValueOfPublicField<GameObject>(prefabInfo, "Prefab");
        _networkPrefabs.Add(prefab!);
      }
    }
  }
}