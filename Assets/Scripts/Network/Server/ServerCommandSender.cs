using System;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Client;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.Network.Server
{
  public class ServerCommandSender : NetworkBehaviour, IServerCommandSender
  {
    public void SendCommand<TDto>(ulong clientId, IServerCommand_ServerSide<TDto> command) where TDto : IDto
    {
      var rpcParams = NetworkUtils.CreateClientRpcParams(clientId);
      ExecuteCommandClientRpc(clientId, command.Key, new DtoWrapper(command.Payload), rpcParams);
    }

    [ClientRpc]
    private void ExecuteCommandClientRpc(ulong clientId, int key, DtoWrapper payload, ClientRpcParams rpcParams = default)
    {
      var serverCommandReceiver = ObjectResolversRegistry.Get(clientId).Resolve<ServerCommandReceiver>();
      serverCommandReceiver.OnCommandReceived(key, payload.Dto);
    }
  }

  public interface IDto : INetworkSerializable
  {
    DtoType Type { get; }
    
    public enum DtoType
    {
      AddedInventoryItemDto = 0
    }
  }
  
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
                 IDto.DtoType.AddedInventoryItemDto => new AddedInventoryItemDto(),
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
  
  public interface IServerCommand_ServerSide<TDto> where TDto : IDto
  {
    int Key { get; }

    TDto Payload { get; }
  }

  public class AddedInventoryItemServerCommand_ServerSide : IServerCommand_ServerSide<AddedInventoryItemDto>
  {
    public const int CommandKey = 0;
    
    public int Key => CommandKey;

    public AddedInventoryItemDto Payload { get; }

    public AddedInventoryItemServerCommand_ServerSide(string itemId, int count) => Payload = new AddedInventoryItemDto(itemId, count);
  }
}