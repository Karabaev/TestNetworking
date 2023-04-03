using Aboba.Characters;
using Aboba.Network.Client.Service;
using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Player
{
  public class PlayerMovement : NetworkBehaviour
  {
    [SerializeField]
    private float _moveSpeed = 3.0f;
    [SerializeField]
    private float _rotationSpeed = 2.0f;

    [SerializeField, HideInInspector]
    private CharacterController _characterController = null!;
    [SerializeField, HideInInspector]
    private CharacterAnimation _characterAnimation = null!;
    
    [Inject]
    private readonly PlayerInput _playerInput = null!;
    // [Inject]
    // private readonly IClientRequestManager _clientRequestManager = null!;
    
    private Vector2 _inputAxis;
    private readonly NetworkVariable<float> _velocity = new();

    private Vector2 _previousInputAxis;

    public override void OnNetworkSpawn()
    {
      if(IsClient)
      {
        _velocity.OnValueChanged += (_, newValue) => _characterAnimation.Speed = newValue / _moveSpeed;
      }
    }

    private void Update()
    {
      if(IsServer)
        ServerUpdate();
      
      if(IsClient && IsOwner)
        ClientInput();
    }

    private void ServerUpdate()
    {
      if(_inputAxis.x != 0.0f)
      {
        var angularVelocity = _inputAxis.x * _rotationSpeed;
        transform.Rotate(transform.up * (angularVelocity * Time.deltaTime));
      }
      
      if(_inputAxis.y != 0.0f)
      {
        var velocity = _inputAxis.y * _moveSpeed;
        _characterController.Move(transform.forward * (velocity * Time.deltaTime));
        _velocity.Value = velocity;
      }
      else
      {
        _velocity.Value = 0.0f;
      }
    }
    
    private async void ClientInput()
    {
      var axis = _playerInput.Axis;

      if(axis != _previousInputAxis)
      {
        UpdateTranslationServerRpc(axis);
        _previousInputAxis = axis;
      }

      if(_playerInput.Attack)
      {
        // var result = await ObjectResolversRegistry.LocalObjectResolver.Resolve<IClientRequestManager>().SendRequestAsync(new AttackClientRequest(OwnerClientId));
        // _clientRequestManager.SendRequestAsync(new AttackClientRequest(OwnerClientId));
        _characterAnimation.Attack();
      }
    }

    [ServerRpc]
    private void UpdateTranslationServerRpc(Vector2 newInputAxis) => _inputAxis = newInputAxis;

    private void OnValidate()
    {
      _characterController = this.RequireComponent<CharacterController>();
      _characterAnimation = this.RequireComponent<CharacterAnimation>();
    }
  }
}