using UnityEngine;

namespace Aboba.Items.Common.Descriptors
{
  [CreateAssetMenu(menuName = "Aboba/Descriptors/EquipmentItem")]
  public class EquipmentItemDescriptor : InventoryItemDescriptor
  {
    [field: SerializeField]
    public GameObject WornPrefab { get; private set; } = null!;
  }
}