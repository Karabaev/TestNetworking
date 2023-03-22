using Aboba.Network.Common;
using Unity.Netcode;

namespace Aboba.Player.Common.Net
{
  public struct ClientConnectedDto : IDto
  {
    private ulong _clientId;
    
    public IDto.DtoType Type => IDto.DtoType.ClientConnected;

    public ulong ClientId => _clientId;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter => serializer.SerializeValue(ref _clientId);

    public ClientConnectedDto(ulong clientId) => _clientId = clientId;
  }
}