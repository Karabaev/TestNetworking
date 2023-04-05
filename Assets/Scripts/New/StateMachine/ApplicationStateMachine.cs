using Cysharp.Threading.Tasks;
using VContainer;

namespace Aboba.New.StateMachine
{
  public class ApplicationStateMachine
  {
    private readonly IObjectResolver _objectResolver;
    
    private IState? _activeState;

    public async UniTask EnterAsync<TState, TContext>(TContext context) where TState : IState<TContext>
    {
      if(_activeState != null)
        await _activeState.ExitAsync();

      var state = CreateState<TState>();
      _activeState = state;
      await state.EnterAsync(context);
    }

    public UniTask EnterAsync<TState>() where TState : IState<DummyStateContext> => EnterAsync<TState, DummyStateContext>(default);

    private TState CreateState<TState>() where TState : IState => _objectResolver.Resolve<TState>();

    public ApplicationStateMachine(IObjectResolver objectResolver)
    {
      _objectResolver = objectResolver;
    }
  }
}