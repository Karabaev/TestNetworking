using Unity.Netcode;
using UnityEngine;

namespace Aboba
{
  public class PlayerInput : NetworkBehaviour
  {
    public Vector2 Axis { get; private set; }
    
    public bool Attack { get; private set; }
    
    private void Awake() => enabled = false;

    public override void OnNetworkSpawn() => enabled = IsClient;

    private void Update()
    {
      Axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      Attack = Input.GetMouseButtonDown(0);
    }
  }
}