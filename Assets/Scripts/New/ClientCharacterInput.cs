using System;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.New
{
  public class ClientCharacterInput : NetworkBehaviour
  {
    private ServerCharacterMovement _characterMovement;

    private void Awake()
    {
      _characterMovement = GetComponent<ServerCharacterMovement>();
    }

    public override void OnNetworkSpawn()
    {
      enabled = IsClient && IsOwner;
    }

    private void Update()
    {
      var horizontal = Input.GetAxis("Horizontal");
      var vertical = Input.GetAxis("Vertical");

      _characterMovement.MoveDirection = new Vector3(horizontal, vertical);
    }
  }
}