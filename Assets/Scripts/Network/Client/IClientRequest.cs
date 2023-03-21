namespace Aboba.Network.Client
{
  public interface IClientRequest<TResponse> where TResponse : IServerResponse
  {
    TResponse GenerateServerResponse();
  }
}