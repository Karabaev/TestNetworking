using Unity.Netcode;

namespace Aboba.Network.Common
{
  public struct DummyDto : IDto
  {
    public IDto.DtoType Type => IDto.DtoType.Dummy;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter { }
  }
}