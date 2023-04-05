using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Aboba.New.StateMachine
{
  public interface IState
  {
    UniTask ExitAsync();
  }

  public abstract class ApplicationState<TContext> : IState
  {
    private IObjectResolver? _objectResolver;
    
    protected T Resolve<T>() => (_objectResolver ??= Object.FindObjectOfType<SceneController>().Container).Resolve<T>();

    public abstract UniTask EnterAsync(TContext context);

    public virtual UniTask ExitAsync() => UniTask.CompletedTask;
  }
}