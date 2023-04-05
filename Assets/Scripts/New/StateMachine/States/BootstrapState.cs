using Cysharp.Threading.Tasks;

namespace Aboba.New.StateMachine.States
{
  public class BootstrapState : IState<DummyStateContext>
  {
    private readonly ApplicationStateMachine _stateMachine;

    public UniTask EnterAsync(DummyStateContext payload)
    {
      return _stateMachine.EnterAsync<GameBootstrapState>();
    }

    public UniTask ExitAsync() => UniTask.CompletedTask;

    public BootstrapState(ApplicationStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
  }
}