using VContainer;

namespace Aboba.Network.Common
{
  public interface IClientRequest
  {
    int Key { get; }
    
    /// <summary>
    /// Заполняется на клиенте.
    /// </summary>
    IDto Payload { get; }

    /// <summary>
    /// Исполняется на сервере.
    /// </summary>
    /// <param name="objectResolver">Серверный резолвер.</param>
    /// <param name="payload">Данные с клиента.</param>
    IDto Execute(IObjectResolver objectResolver, IDto payload);
  }
}