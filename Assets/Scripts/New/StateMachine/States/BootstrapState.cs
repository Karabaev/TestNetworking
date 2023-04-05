using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Aboba.New.StateMachine.States
{
  [UsedImplicitly]
  public class BootstrapState : ApplicationState<DummyStateContext>
  {
    private readonly ApplicationStateMachine _stateMachine;
    private readonly SceneService _sceneService;
    
    public override async UniTask EnterAsync(DummyStateContext payload)
    {
      await _sceneService.OpenSceneAsync("Bootstrap");
      await _stateMachine.EnterAsync<GameBootstrapState>();
    }

    public BootstrapState(ApplicationStateMachine stateMachine, SceneService sceneService)
    {
      _stateMachine = stateMachine;
      _sceneService = sceneService;
    }
  }
}