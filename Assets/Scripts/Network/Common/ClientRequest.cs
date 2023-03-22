using VContainer;

namespace Aboba.Network.Common
{
  public abstract class ClientRequest<TPayload> : IClientRequest where TPayload : IDto
  {
    public abstract int Key { get; }
    
    public IDto Payload { get; }

    public TPayload TypedPayload => (TPayload)Payload;

    /// <summary>
    /// Исполняется на сервере.
    /// </summary>
    /// <param name="objectResolver">Серверный резолвер.</param>
    /// <param name="payload"></param>
    public abstract IDto Execute(IObjectResolver objectResolver, IDto payload);

    protected ClientRequest(IDto payload) => Payload = payload;
    
    protected ClientRequest() => Payload = null!;
  }
}