using Aboba.Network.Common;
using VContainer;

namespace Aboba.Network.Client
{
  public interface IServerCommand_ClientSide
  {
    void Execute(IDto payload, IObjectResolver objectResolver);
  }
}