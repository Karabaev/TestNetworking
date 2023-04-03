using Aboba.Network.Common;
using Unity.Netcode;

namespace Aboba.Player
{
  public struct AttackDto : IDto
  {
    private ulong _userId;
    
    public IDto.DtoType Type => IDto.DtoType.Attack;

    public ulong UserId => _userId;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref _userId);
    }
    
    public AttackDto(ulong userId) => _userId = userId;

  }
}