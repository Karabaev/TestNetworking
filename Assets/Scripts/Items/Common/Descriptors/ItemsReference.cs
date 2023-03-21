using System.Collections.Generic;
using UnityEngine;

namespace Aboba.Items.Common.Descriptors
{
  [CreateAssetMenu(menuName = "Aboba/Descriptors/ItemsReference")]
  public class ItemsReference : ScriptableObject
  {
    [SerializeField]
    private ItemDescriptor[] _items = null!;

    public IReadOnlyList<ItemDescriptor> Items => _items;
  }
}