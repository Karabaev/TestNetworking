using Cysharp.Threading.Tasks;

namespace Aboba.New.StateMachine.States
{
  public class GameLoopState : IState<DummyStateContext>
  {
    public UniTask EnterAsync(DummyStateContext payload) => UniTask.CompletedTask;

    public UniTask ExitAsync() => UniTask.CompletedTask;
  }
}