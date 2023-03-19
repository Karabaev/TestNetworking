using System.Collections.Generic;
using Aboba.Player;
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

      if(networkManager.IsServer)
      {
        var spawnPoint = Vector3.zero;
        var hero = Instantiate(_heroPrefab, spawnPoint, Quaternion.identity);
        Container.InjectGameObject(hero.gameObject); // todokmo создать пул геймобжектов, указать хендлер префабов
        hero.SpawnWithOwnership(clientId, true);
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
    }
  }
}