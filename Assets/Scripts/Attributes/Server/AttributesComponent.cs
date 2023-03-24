using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aboba.Attributes.Server
{
  public class AttributesComponent : MonoBehaviour
  {
    public Attribute Attack { get; private set; } = null!;
    public Attribute Armor { get; private set; } = null!;
    public Attribute MaxHealth { get; private set; } = null!;
    
    private float _currentHealth;
    public float CurrentHealth
    {
      get => _currentHealth;
      private set
      {
        if(_currentHealth == value)
          return;

        var newValue = Mathf.Clamp(value, 0, MaxHealth.CurrentValue);
        var delta = newValue - _currentHealth;
        _currentHealth = newValue;
        CurrentHpChanged?.Invoke(delta);
      }
    }

    private readonly Dictionary<string, Attribute> _attributes = new();
    public IReadOnlyDictionary<string, Attribute> Attributes => _attributes;

    public event Action? AttributesChanged;

    public event Action<float>? CurrentHpChanged;
    
    public void Initialize(IReadOnlyDictionary<string, float> initialAttributes)
    {
      foreach(var ip in initialAttributes)
        _attributes[ip.Key] = new Attribute(ip.Key, ip.Value);
      
      Attack = Attributes["attack"];
      Armor = Attributes["armor"];
      MaxHealth = Attributes["maxHealth"];
      
      SetHpToMax();
    }

    public void AddTempModifiers(IEnumerable<AttributeModifier> modifiers)
    {
      var changed = false;

      foreach(var modifier in modifiers)
      {
        var attribute = Attributes[modifier.AttributeId];
        attribute.AddTempModifier(modifier);
        changed = true;
      }

      if(!changed)
        return;
      
      AttributesChanged?.Invoke();
    }

    public void AddTempModifier(AttributeModifier modifier)
    {
      var attribute = Attributes[modifier.AttributeId];
      attribute.AddTempModifier(modifier);
      AttributesChanged?.Invoke();
    }

    public void AddPersistentModifiers(IEnumerable<AttributeModifier> modifiers)
    {
      var changed = false;
      
      foreach(var modifier in modifiers)
      {
        var attribute = Attributes[modifier.AttributeId];
        attribute.AddPersistentModifier(modifier);
        changed = true;
      }

      if(!changed)
        return;
      
      AttributesChanged?.Invoke();
    }

    public void AddPersistentModifier(AttributeModifier modifier)
    {
      var attribute = Attributes[modifier.AttributeId];
      attribute.AddPersistentModifier(modifier);
      AttributesChanged?.Invoke();
    }

    public void RemoveTempModifiers(IEnumerable<AttributeModifier> modifiers)
    {
      foreach(var modifier in modifiers)
      {
        var attribute = Attributes[modifier.AttributeId];
        attribute.RemoveModifier(modifier);
      }

      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth.CurrentValue);
      AttributesChanged?.Invoke();
    }

    public void RemoveTempModifier(AttributeModifier modifier)
    {
      var attribute = Attributes[modifier.AttributeId];
      attribute.RemoveModifier(modifier);
      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth.CurrentValue);
      AttributesChanged?.Invoke();
    }

    public void RemovePersistentModifier(AttributeModifier modifier)
    {
      var attribute = Attributes[modifier.AttributeId];
      attribute.RemovePersistentModifier(modifier);
      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth.CurrentValue);
      AttributesChanged?.Invoke();
    }

    public void RemovePersistentModifiers(IEnumerable<AttributeModifier> modifiers)
    {
      foreach(var modifier in modifiers)
      {
        var attribute = Attributes[modifier.AttributeId];
        attribute.RemovePersistentModifier(modifier);
      }

      CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth.CurrentValue);
      AttributesChanged?.Invoke();
    }
    
    public void ApplyHeal(int inputHeal) => CurrentHealth = Mathf.Clamp(CurrentHealth + inputHeal, 0, MaxHealth.CurrentValue);

    public void SetHpToMax() => CurrentHealth = MaxHealth.CurrentValue;

    public bool ApplyDamage(float value)
    {
      if(CurrentHealth <= value)
      {
        CurrentHealth = 0;
        return true;
      }

      CurrentHealth -= value;
      return false;
    }
  }
}