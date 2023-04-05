using Aboba.New.Framework.Reactive;

namespace Aboba.New.Fight.World
{
  public class UnitModel
  {
    public string Name { get; }
    
    public ReactiveProperty<int> Count { get; }

    public float MaxHealth { get; }
    
    public ReactiveProperty<float> CurrentHealth { get; }
    
    public ReactiveProperty<float> Damage { get; }

    public UnitModel(string name, int count, float maxHealth, float currentHealth, float damage)
    {
      Name = name;
      Count = new ReactiveProperty<int>(count);
      MaxHealth = maxHealth;
      CurrentHealth = new ReactiveProperty<float>(currentHealth);
      Damage = new ReactiveProperty<float>(damage);
    }
  }
}