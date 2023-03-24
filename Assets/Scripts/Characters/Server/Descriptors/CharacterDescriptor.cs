using Unity.Netcode;
using UnityEngine;

namespace Aboba.Characters.Server.Descriptors
{
  [CreateAssetMenu(menuName = "Aboba/Descriptors/NewCharacter", fileName = "dsCharacter")]
  public class CharacterDescriptor : ScriptableObject
  {
    [field: SerializeField]
    public string Id { get; private set; } = null!;
    
    [field: SerializeField]
    public NetworkObject Prefab { get; private set; } = null!;
    
    [field: SerializeField]
    public float MaxHp { get; private set; }
    
    [field: SerializeField]
    public float Attack { get; private set; }
    
    [field: SerializeField]
    public float Armor { get; private set; }
  }
}