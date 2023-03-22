using Unity.Netcode;

namespace Aboba.Network
{
  public static class NetworkUtils
  {
    public static ClientRpcParams CreateClientRpcParams(ulong clientId)
    {
      return new ClientRpcParams
             {
               Receive = new ClientRpcReceiveParams(),
               Send = new ClientRpcSendParams { TargetClientIds = new[] { clientId } }
             };
    }
  }
}