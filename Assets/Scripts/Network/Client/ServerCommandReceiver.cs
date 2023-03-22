using System.Collections.Generic;
using Aboba.Items.Client.Services;
using Aboba.Items.Common.Net.Dto;
using Aboba.Network.Server;
using VContainer;

namespace Aboba.Network.Client
{
  public class ServerCommandReceiver
  {
    private readonly Dictionary<int, IServerCommand_ClientSide> _commandsRegistry = new();

    public void OnCommandReceived<TDto>(int key, TDto payload) where TDto : IDto
    {
      _commandsRegistry[key].Execute(payload);
    }

    public ServerCommandReceiver(IObjectResolver objectResolver)
    {
      _commandsRegistry[AddedInventoryItemServerCommand_ServerSide.CommandKey] = new AddedInventoryItemServerCommand_ClientSide(objectResolver);
    }
  }
  
  public interface IServerCommand_ClientSide
  {
    void Execute(IDto payload);
  }

  public class AddedInventoryItemServerCommand_ClientSide : IServerCommand_ClientSide
  {
    private readonly IObjectResolver _objectResolver;
    
    public void Execute(IDto payload)
    {
      var dto = (AddedInventoryItemDto)payload;
      _objectResolver.Resolve<ClientInventoryService>().AddItems(dto.ItemId, dto.Count);
    }

    public AddedInventoryItemServerCommand_ClientSide(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
    }
  }
}