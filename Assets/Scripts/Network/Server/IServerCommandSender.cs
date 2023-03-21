namespace Aboba.Network.Server
{
  public interface IServerCommandSender
  {
    void SendCommand<TDto>(ulong clientId, IServerCommand_ServerSide<TDto> command) where TDto : IDto;
  }
}