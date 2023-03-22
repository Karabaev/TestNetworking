using Aboba.Network.Common;
using VContainer;

namespace Aboba.Network.Server
{
  public interface IClientRequest_ServerSide
  {
    IDto Execute(IObjectResolver objectResolver, IDto payload);
  }
}