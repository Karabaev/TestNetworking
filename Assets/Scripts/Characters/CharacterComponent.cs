using Aboba.Attributes.Server;
using Aboba.Characters.Client.UI;
using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.Characters
{
  public class CharacterComponent : NetworkBehaviour
  {
    [SerializeField, HideInInspector]
    private AttributesComponent _attributesComponent = null!;
    [SerializeField, HideInInspector]
    private CharacterStatusBar _characterStatusBar = null!;
    [SerializeField, HideInInspector]
    private CharacterAnimation _characterAnimation = null!;
      
    private readonly NetworkVariable<float> _attack = new();
    private readonly NetworkVariable<float> _armor = new();
    private readonly NetworkVariable<float> _maxHp = new();
    private readonly NetworkVariable<float> _currentHp = new();

    public float CurrentAttack => _attack.Value;
    
    public float CurrentArmor => _armor.Value;
    
    public float CurrentMaxHp => _maxHp.Value;
    
    public float CurrentHp => _currentHp.Value;

    public string Name
    {
      set => _characterStatusBar.Name = value;
    }

    public override void OnNetworkSpawn()
    {
      if(IsClient)
      {
        _maxHp.OnValueChanged += OnMaxHpChanged;
        _currentHp.OnValueChanged += OnCurrentHpChanged;
      }
      
      if(IsServer)
      {
        _attributesComponent.AttributesChanged += OnAttributesChanged;
        _attributesComponent.CurrentHpChanged += OnAttributesCurrentHpChanged;

        _attack.Value = _attributesComponent.Attack.CurrentValue;
        _armor.Value = _attributesComponent.Armor.CurrentValue;
        _maxHp.Value = _attributesComponent.MaxHealth.CurrentValue;
        _currentHp.Value = _attributesComponent.CurrentHealth;
      }
    }

    public void Attack()
    {
      _characterAnimation.Attack();
      GetComponentInChildren<Weapon>().Attacking = true;
    }

    public void ApplyDamage(float damage, bool ignoreArmor)
    {
      if(IsServer)
      {
        var actualArmor = ignoreArmor ? 0 : _attributesComponent.Armor.CurrentValue;
        var calculatedDamage = Mathf.Clamp(damage - actualArmor, 0, float.MaxValue);
        _attributesComponent.ApplyDamage(calculatedDamage);
        
        if(calculatedDamage <= 0)
          return;
      }
      
      _characterAnimation.GetHit();
    }
    
    private void OnAttributesCurrentHpChanged(float newHp) => _currentHp.Value = newHp;

    private void OnAttributesChanged()
    {
      _attack.Value = _attributesComponent.Attack.CurrentValue;
      _armor.Value = _attributesComponent.Armor.CurrentValue;
      _maxHp.Value = _attributesComponent.MaxHealth.CurrentValue;
    }

    private void OnCurrentHpChanged(float previousValue, float newValue) => _characterStatusBar.UpdateHp(_currentHp.Value, _maxHp.Value);

    private void OnMaxHpChanged(float previousValue, float newValue) => _characterStatusBar.UpdateHp(_currentHp.Value, _maxHp.Value);

    private void OnValidate()
    {
      _attributesComponent = this.RequireComponent<AttributesComponent>();
      _characterStatusBar = this.RequireComponentInChildren<CharacterStatusBar>();
      _characterAnimation = this.RequireComponent<CharacterAnimation>();
    }
  }
}