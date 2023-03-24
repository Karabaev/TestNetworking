using System.Collections.Generic;
using System.Linq;
using Aboba.Attributes.Server;
using Aboba.Characters.Server.Descriptors;
using Aboba.Utils;
using JetBrains.Annotations;
using Unity.Netcode;
using UnityEngine;

namespace Aboba.Characters.Server
{
  [UsedImplicitly]
  public class ServerHeroService
  {
    private readonly CharacterRegistry _characterRegistry;
    
    public NetworkObject CreateCharacter(ulong clientId, string characterId, Vector3 position, Quaternion rotation)
    {
      var character = CreateCharacterInternal(characterId, position, rotation);
      character.SpawnAsPlayerObject(clientId, true);
      return character;
    }

    public NetworkObject CreateCharacter(string characterId, Vector3 position, Quaternion rotation)
    {
      var character = CreateCharacterInternal(characterId, position, rotation);
      character.SpawnWithOwnership(NetworkManager.ServerClientId, true);
      return character;
    }

    private NetworkObject CreateCharacterInternal(string characterId, Vector3 position, Quaternion rotation)
    {
      var descriptor = _characterRegistry.Characters.First(c => c.Id == characterId);
      var character = Object.Instantiate(descriptor.Prefab);
      character.transform.SetPositionAndRotation(position, rotation);

      character.RequireComponent<AttributesComponent>().Initialize(new Dictionary<string, float>
                                                                    {
                                                                      { "attack", descriptor.Attack },
                                                                      { "armor", descriptor.Armor },
                                                                      { "maxHealth", descriptor.MaxHp }
                                                                    });
      return character;
    }
    
    public ServerHeroService(CharacterRegistry characterRegistry) => _characterRegistry = characterRegistry;
  }
}