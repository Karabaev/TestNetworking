namespace Aboba.Network.Server
{
  public interface IServerCommandManager
  {
    void NotifyInventoryItemAdded(ulong clientId, string itemId, int count);
  }
}