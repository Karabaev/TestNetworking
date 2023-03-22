using Aboba.Network.Common;

namespace Aboba.Network.Client
{
  public interface IServerCommand_ClientSide
  {
    void Execute(IDto payload);
  }
}