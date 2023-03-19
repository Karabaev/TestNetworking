using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Player
{
  public class CharacterMovement : NetworkBehaviour
  {
    [Inject]
    private PlayerInput _playerInput = null!;
    
    private readonly NetworkVariable<Vector2> _inputAxis = new();

    private Vector2 _previousInputAxis;
    
    private void ServerUpdate()
    {
      var direction = new Vector3(_inputAxis.Value.x, 0, _inputAxis.Value.y).normalized;
      var translation = direction * (5 * Time.deltaTime);
      transform.Translate(translation);
    }

    private void Update()
    {
      if(IsServer)
        ServerUpdate();
      
      if(IsClient && IsOwner)
        ClientUpdate();
    }

    private void ClientUpdate()
    {
      var axis = _playerInput.Axis;

      if(axis == _previousInputAxis)
        return;
      
      UpdateTranslationServerRpc(axis);
      _previousInputAxis = axis;
    }

    [ServerRpc]
    private void UpdateTranslationServerRpc(Vector2 newInputAxis)
    {
      _inputAxis.Value = newInputAxis;
    }
  }
}