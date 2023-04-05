using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace Aboba.New.StateMachine.States
{
  [UsedImplicitly]
  public class GameBootstrapState : ApplicationState<DummyStateContext>
  {
    private readonly SceneService _sceneService;
    private readonly ApplicationStateMachine _stateMachine;
    
    private SceneController _sceneController = null!;
    
    public override async UniTask EnterAsync(DummyStateContext context)
    {
      await _sceneService.OpenSceneAsync("Game");
      _sceneController = Object.FindObjectOfType<SceneController>();
      
      await _stateMachine.EnterAsync<GameLoopState>();
    }

    public GameBootstrapState(SceneService sceneService, ApplicationStateMachine stateMachine)
    {
      _sceneService = sceneService;
      _stateMachine = stateMachine;
    }
  }
}