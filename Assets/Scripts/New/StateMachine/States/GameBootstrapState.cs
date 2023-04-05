using Cysharp.Threading.Tasks;

namespace Aboba.New.StateMachine.States
{
  public class GameBootstrapState : IState<DummyStateContext>
  {
    private readonly SceneService _sceneService;
    private readonly ApplicationStateMachine _stateMachine;
    
    public async UniTask EnterAsync(DummyStateContext context)
    {
      await _sceneService.OpenSceneAsync("Game");
      _stateMachine.EnterAsync<GameLoopState>();
    }

    public UniTask ExitAsync() => UniTask.CompletedTask;

    public GameBootstrapState(SceneService sceneService, ApplicationStateMachine stateMachine)
    {
      _sceneService = sceneService;
      _stateMachine = stateMachine;
    }
  }
}