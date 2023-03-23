using VContainer;

namespace Aboba.Network.Common
{
  public interface IServerCommand
  {
    int Key { get; }

    IDto Payload { get; }
    
    void Execute(IDto payload, IObjectResolver objectResolver);
  }
}