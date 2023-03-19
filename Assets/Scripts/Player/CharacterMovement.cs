using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Player
{
  public class CharacterMovement : NetworkBehaviour
  {
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    
    [SerializeField]
    private float _moveSpeed = 3.0f;
    [SerializeField]
    private float _rotationSpeed = 2.0f;

    [SerializeField, HideInInspector]
    private CharacterController _characterController = null!;
    [SerializeField, HideInInspector]
    private Animator _animator = null!;
    
    [Inject]
    private PlayerInput _playerInput = null!;
    
    private readonly NetworkVariable<Vector2> _inputAxis = new();
    private readonly NetworkVariable<float> _velocity = new();

    private Vector2 _previousInputAxis;
    
    public override void OnNetworkSpawn()
    {
      if(IsClient)
      {
        _velocity.OnValueChanged += (_, newValue) => _animator.SetFloat(SpeedHash, newValue / _moveSpeed);
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
      if(_inputAxis.Value.x != 0.0f)
      {
        var angularVelocity = _inputAxis.Value.x * _rotationSpeed;
        transform.Rotate(transform.up * (angularVelocity * Time.deltaTime));
      }
      
      if(_inputAxis.Value.y != 0.0f)
      {
        var velocity = _inputAxis.Value.y * _moveSpeed;
        _characterController.Move(transform.forward * (velocity * Time.deltaTime));
        _velocity.Value = velocity;
      }
      else
      {
        _velocity.Value = 0.0f;
      }
    }
    
    private void ClientInput()
    {
      var axis = _playerInput.Axis;
      
      if(axis == _previousInputAxis)
        return;
      
      UpdateTranslationServerRpc(axis);
      _previousInputAxis = axis;
    }

    [ServerRpc]
    private void UpdateTranslationServerRpc(Vector2 newInputAxis) => _inputAxis.Value = newInputAxis;

    private void OnValidate()
    {
      _characterController = this.RequireComponent<CharacterController>();
      _animator = this.RequireComponentInChildren<Animator>();
    }
  }
}