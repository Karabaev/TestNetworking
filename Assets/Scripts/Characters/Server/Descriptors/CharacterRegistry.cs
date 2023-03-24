using System.Collections.Generic;
using UnityEngine;

namespace Aboba.Characters.Server.Descriptors
{
  [CreateAssetMenu(menuName = "Aboba/Descriptors/CharacterRegistry")]
  public class CharacterRegistry : ScriptableObject
  {
    [SerializeField]
    private List<CharacterDescriptor> _characters = null!;

    public IReadOnlyList<CharacterDescriptor> Characters => _characters;
  }
}