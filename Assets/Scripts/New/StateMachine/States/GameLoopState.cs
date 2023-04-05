using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Aboba.New.StateMachine.States
{
  [UsedImplicitly]
  public class GameLoopState : ApplicationState<DummyStateContext>
  {
    public override UniTask EnterAsync(DummyStateContext payload) => UniTask.CompletedTask;
  }
}