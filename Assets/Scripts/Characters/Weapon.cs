using UnityEngine;

namespace Aboba.Characters
{
  public class Weapon : MonoBehaviour
  {
    private CharacterComponent _owner = null!;

    private void Awake()
    {
      _owner = GetComponentInParent<CharacterComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if(!other.TryGetComponent<AttackableComponent>(out var attackable))
        return;
      
      attackable.ApplyDamage(10, false);
    }
  }
}