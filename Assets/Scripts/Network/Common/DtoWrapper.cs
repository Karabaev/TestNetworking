using System;
using Aboba.Items.Common.Net.Dto;
using Aboba.Player.Common.Net;
using Unity.Netcode;

namespace Aboba.Network.Common
{
  public struct DtoWrapper : INetworkSerializable
  {
    private IDto.DtoType _type;
    private IDto _dto;

    public IDto Dto => _dto;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref _type);
      if(serializer.IsReader)
      {
        _dto = _type switch
               {
                 IDto.DtoType.Dummy => new DummyDto(),
                 IDto.DtoType.AddedInventoryItemDto => new AddedInventoryItemDto(),
                 IDto.DtoType.ClientConnected => new ClientConnectedDto(),
                 IDto.DtoType.GetUserInventory => new GetUserInventoryDto(),
                 IDto.DtoType.GetUserInventoryResponse => new GetUserInventoryResponse(),
                 _ => throw new ArgumentOutOfRangeException()
               };
      }
      _dto.NetworkSerialize(serializer);
    }

    public DtoWrapper(IDto dto)
    {
      _dto = dto;
      _type = dto.Type;
    }
  }
}