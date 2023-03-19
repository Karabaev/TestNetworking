using System;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.New
{
  public class CharacterMovement : NetworkBehaviour
  {
    private readonly NetworkVariable<Vector2> _translation = new();

    private Vector2 _previousTranslation;
    
    private void ServerUpdate()
    {
      transform.Translate(new Vector3(_translation.Value.x * Time.deltaTime, 0.0f, _translation.Value.y * Time.deltaTime));
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
      var translation = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

      if(translation == _previousTranslation)
        return;

      UpdateTranslationServerRpc(translation);
      _previousTranslation = translation;
    }

    [ServerRpc]
    private void UpdateTranslationServerRpc(Vector2 newTranslation)
    {
      _translation.Value = newTranslation;
    }
  }
}