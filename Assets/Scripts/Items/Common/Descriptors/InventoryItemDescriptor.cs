using UnityEngine;

namespace Aboba.Items.Common.Descriptors
{
  [CreateAssetMenu(menuName = "Aboba/Descriptors/InventoryItem")]
  public class InventoryItemDescriptor : ItemDescriptor
  {
    [field: SerializeField]
    public Sprite Icon { get; private set; } = null!;
  }
}