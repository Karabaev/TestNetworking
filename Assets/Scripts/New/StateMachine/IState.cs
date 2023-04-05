using Cysharp.Threading.Tasks;

namespace Aboba.New.StateMachine
{
  public interface IState
  {
    UniTask ExitAsync();
  }
  
  public interface IState<TContext> : IState
  {
    UniTask EnterAsync(TContext context);
  }
}