using Aboba.New.Fight.World;
using Aboba.New.Framework;
using UnityEngine;

namespace Aboba.New.Fight
{
  public class UnitEntity : Entity<UnitModel, UnitView, UnitPresenter>
  {
    private readonly string _name;
    private readonly int _count;
    private readonly float _maxHealth;
    private readonly float _currentHealth;
    private readonly float _damage;
    
    protected override UnitModel CreateModel() => new(_name, _count, _maxHealth, _currentHealth, _damage);

    protected override UnitView CreateView() => Object.Instantiate<UnitView>(null);

    protected override UnitPresenter CreatePresenter(UnitModel model, UnitView view) => new(model, view);

    public UnitEntity(string name, int count, float maxHealth, float currentHealth, float damage)
    {
      _name = name;
      _count = count;
      _maxHealth = maxHealth;
      _currentHealth = currentHealth;
      _damage = damage;
    }
  }
}