using System;
using Aboba.Attributes.Server;
using Aboba.Utils;
using UnityEngine;

namespace Aboba.Characters
{
  [DisallowMultipleComponent]
  public class AttackableComponent : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private AttributesComponent _attributesComponent = null!;

    public bool IsDead { get; private set; }

    public event Action? Damaged;

    public void Reset() => IsDead = false;

    public void ApplyDamage(float damage, bool ignoreArmor)
    {
      if(IsDead)
        return;

      var actualArmor = ignoreArmor ? 0 : _attributesComponent.Armor.CurrentValue;
      var calculatedDamage = Mathf.Clamp(damage - actualArmor, 0, float.MaxValue);
      IsDead = _attributesComponent.ApplyDamage(calculatedDamage);

      if(calculatedDamage > 0)
        Damaged?.Invoke();
    }

    public void Kill() => ApplyDamage(_attributesComponent.CurrentHealth, true);

    private void OnValidate() => _attributesComponent = this.RequireComponent<AttributesComponent>();
  }
}