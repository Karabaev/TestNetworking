using Aboba.Items.Common.Descriptors;
using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.Items.Client
{
  public class ClientLootObject : NetworkBehaviour
  {
    private const int RotationSpeed = 30;
    
    [field: SerializeField]
    public ItemDescriptor ItemDescriptor { get; private set; } = null!;

    [SerializeField, HideInInspector]
    private Transform _graphics = null!;
    
    private void Awake() => enabled = false;

    public override void OnNetworkSpawn() => enabled = IsClient;

    private void Update() => _graphics.Rotate(0.0f, RotationSpeed * Time.deltaTime, 0.0f);

    private void OnValidate()
    {
      _graphics = this.RequireComponentInChildrenOnly<Transform>();
    }
  }
}