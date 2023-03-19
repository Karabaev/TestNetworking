using Unity.Netcode;
using UnityEngine;

namespace Aboba
{
  public class PlayerMovement : NetworkBehaviour
  {
    [SerializeField]
    private float _speed = 0.5f;

    private void Update()
    {
      if(!IsOwner)
        return;

      var xInput = Input.GetAxis("Horizontal");
      var yInput = Input.GetAxis("Vertical");

      var moveDirection = new Vector3(xInput, 0, yInput).normalized;
      transform.Translate(_speed * Time.deltaTime * moveDirection);
    }
  }
}