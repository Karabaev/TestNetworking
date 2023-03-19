using System;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.New
{
  public class ServerCharacterMovement : NetworkBehaviour
  {
    private bool _movePerformed;
    
    private Vector3 _moveDirection;
    public Vector3 MoveDirection
    {
      private get => _moveDirection;
      set
      {
        _moveDirection = value.normalized;
        _movePerformed = false;
      }
    }

    private void Awake()
    { 
      enabled = false;
    }

    public override void OnNetworkSpawn()
    {
      enabled = IsServer;
    }

    private void Update()
    {
      if(_movePerformed)
        return;

      transform.Translate(3 * Time.deltaTime * MoveDirection);
      _movePerformed = true;
    }
  }
}