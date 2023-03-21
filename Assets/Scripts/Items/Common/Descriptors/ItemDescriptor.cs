using UnityEngine;

namespace Aboba.Items.Common.Descriptors
{
  public class ItemDescriptor : ScriptableObject
  {
    [field: SerializeField]
    public string Id { get; private set; } = null!;
    
    [field: SerializeField]
    public string Name { get; private set; } = null!;

    [field: SerializeField]
    public string Description { get; private set; } = null!;
  }
}