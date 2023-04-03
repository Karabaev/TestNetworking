using Aboba.Utils;
using Unity.Netcode.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aboba.Characters
{
  public class CharacterAnimation : MonoBehaviour
  {
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int AttackTypeHash = Animator.StringToHash("AttackType");
    private static readonly int GetHitHash = Animator.StringToHash("GetHit");
    
    private Animator _animator = null!;
    private NetworkAnimator _networkAnimator = null!;
    
    public float Speed
    {
      set => _animator.SetFloat(SpeedHash, value);
    }

    private void Awake()
    {
      _animator = this.RequireComponentInChildren<Animator>();
      _networkAnimator = this.RequireComponentInChildren<NetworkAnimator>();
    }

    public void Attack()
    {
      var value = Random.Range(0, 2);
      
      _animator.SetInteger(AttackTypeHash, value);
      _networkAnimator.SetTrigger(AttackHash);
    }

    public void GetHit() => _networkAnimator.SetTrigger(GetHitHash);
  }
}