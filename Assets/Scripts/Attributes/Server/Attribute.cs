using System;
using System.Collections.Generic;
using Aboba.Utils;
using UnityEngine;

namespace Aboba.Attributes.Server
{
  [Serializable]
  public class Attribute
  {
    private readonly float _baseValue;
    private readonly List<AttributeModifier> _temporaryModifiers;
    private readonly List<AttributeModifier> _persistentModifiers;

    public string DescriptorId { get; }
    
    public float PersistentValue { get; private set; }
    public float TemporaryValue { get; private set; }
    public float CurrentValue => PersistentValue + TemporaryValue;

    public void AddTempModifier(AttributeModifier modifier)
    {
      if(modifier.AttributeId != DescriptorId)
        return;
      
      _temporaryModifiers.Add(modifier);
      RecalculateTemporaryValue();
    }

    public void AddPersistentModifier(AttributeModifier modifier)
    {
      if(modifier.AttributeId != DescriptorId)
        return;
      
      _persistentModifiers.Add(modifier);
      RecalculatePersistentValue();
      RecalculateTemporaryValue();
    }

    public void RemoveModifier(AttributeModifier modifier)
    {
      if(modifier.AttributeId != DescriptorId)
        return;
      
      _temporaryModifiers.Remove(modifier);
      RecalculateTemporaryValue();
    }

    public void RemovePersistentModifier(AttributeModifier modifier)
    {
      if(modifier.AttributeId != DescriptorId)
        return;
      
      _persistentModifiers.Remove(modifier);
      RecalculatePersistentValue();
      RecalculateTemporaryValue();
    }

    private void RecalculatePersistentValue()
    {
      var result = _baseValue;
      _persistentModifiers.Sort((x, y) => x.AttributeOperationType.CompareTo(y.AttributeOperationType));
      
      foreach (var modifier in _persistentModifiers)
      {
        if(modifier.AttributeOperationType == AttributeOperationType.Override)
        {
          result = modifier.Value;
          break;
        }
        
        switch (modifier.AttributeOperationType)
        {
          case AttributeOperationType.Add:
            result += modifier.Value;
            break;
          case AttributeOperationType.Multi:
            result *= modifier.Value;
            break;
          case AttributeOperationType.Percent:
            result += MathUtils.PercentFrom(result, modifier.Value);
            break;
        }
      }
      
      PersistentValue = Mathf.Max(0, result);
    }

    private void RecalculateTemporaryValue()
    {
      var result = 0.0f;
      _temporaryModifiers.Sort((x, y) => x.AttributeOperationType.CompareTo(y.AttributeOperationType));
      
      foreach (var modifier in _temporaryModifiers)
      {
        if(modifier.AttributeOperationType == AttributeOperationType.Override)
        {
          result = modifier.Value;
          break;
        }
        
        switch (modifier.AttributeOperationType)
        {
          case AttributeOperationType.Add:
            result += modifier.Value;
            break;
          case AttributeOperationType.Multi:
            result += PersistentValue * modifier.Value;
            break;
          case AttributeOperationType.Percent:
            result += MathUtils.PercentFrom(PersistentValue, modifier.Value);
            break;
        }
      }

      TemporaryValue = result;
    }

    public Attribute(string descriptorId, float baseValue)
    {
      if (baseValue < 0)
        throw new ArgumentOutOfRangeException($"{nameof(baseValue)} cannot be less than zero");
      
      DescriptorId = descriptorId;
      _baseValue = baseValue;
      _temporaryModifiers = new List<AttributeModifier>();
      _persistentModifiers = new List<AttributeModifier>();
      RecalculatePersistentValue();
      RecalculateTemporaryValue();
    }
  }
}