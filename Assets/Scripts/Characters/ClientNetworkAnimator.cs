using Unity.Netcode.Components;

namespace Aboba.Characters
{
  public class ClientNetworkAnimator : NetworkAnimator
  {
    protected override bool OnIsServerAuthoritative() => false;
  }
}