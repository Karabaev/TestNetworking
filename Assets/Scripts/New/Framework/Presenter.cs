using UnityEngine;

namespace Aboba.New.Framework
{
  public abstract class Presenter<TModel, TView> : IPresenter
  {
    protected TModel Model { get; }
    
    protected TView View { get; }

    public virtual void OnStart() { }

    public virtual void OnDestroy() => Object.Destroy(View as Object);

    protected Presenter(TModel model, TView view)
    {
      Model = model;
      View = view;
    }
  }
}