using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;

namespace Aboba.New.StateMachine
{
  [UsedImplicitly]
  public class ApplicationStateMachine
  {
    private readonly IObjectResolver _objectResolver;
    
    private IState? _activeState;

    public async UniTask EnterAsync<TState, TContext>(TContext context) where TState : ApplicationState<TContext>
    {
      if(_activeState != null)
        await _activeState.ExitAsync();

      var state = _objectResolver.Resolve<TState>();
      _activeState = state;
      await state.EnterAsync(context);
    }

    public UniTask EnterAsync<TState>() where TState : ApplicationState<DummyStateContext> => EnterAsync<TState, DummyStateContext>(default);

    public ApplicationStateMachine(IObjectResolver objectResolver) => _objectResolver = objectResolver;
  }
}