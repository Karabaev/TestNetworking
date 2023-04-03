using System;
using Aboba.Attributes.Server;
using Aboba.Utils;
using UnityEngine;

namespace Aboba.Characters.Server
{
  [DisallowMultipleComponent]
  public class AttackableComponent : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private AttributesComponent _attributesComponent = null!;
    [SerializeField, HideInInspector]
    private CharacterAnimation _characterAnimation = null!;
    
    public event Action? Damaged;

    public void ApplyDamage(float damage, bool ignoreArmor)
    {
      var actualArmor = ignoreArmor ? 0 : _attributesComponent.Armor.CurrentValue;
      var calculatedDamage = Mathf.Clamp(damage - actualArmor, 0, float.MaxValue);
      _attributesComponent.ApplyDamage(calculatedDamage);

      if(calculatedDamage <= 0)
        return;
      
      Damaged?.Invoke();
      _characterAnimation.GetHit();
    }

    public void Kill() => ApplyDamage(_attributesComponent.CurrentHealth, true);

    private void OnValidate() => _attributesComponent = this.RequireComponent<AttributesComponent>();
  }
}