using Aboba.Characters.Server;
using UnityEngine;

namespace Aboba.Characters
{
  /// <summary>
  /// Хочется, чтобы это работало на сервере
  /// </summary>
  public class Weapon : MonoBehaviour
  {
    private CharacterComponent _owner = null!;

    public bool Attacking { get; set; }
    
    private void Awake()
    {
      _owner = GetComponentInParent<CharacterComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if(!other.TryGetComponent<CharacterComponent>(out var character))
        return;
      
      character.ApplyDamage(10, false);
    }
  }
}